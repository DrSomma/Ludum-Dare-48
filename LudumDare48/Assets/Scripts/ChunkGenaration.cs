using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenaration : MonoBehaviour
{
    public int chunkSizeY = 20;
    public int OffsetY;
    public GameObject defaultPrefab;
    protected int mapSizeX;
    private WorldTile[,] chunkTiles;

    public void ClearLayer()
    {
        GameObject[] toDestroy = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            toDestroy[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject gameObject in toDestroy)
        {
            DestroyImmediate(gameObject);
        }
    }

    public virtual void GenerateChunk(int offsetY, int seed, List<NoiceMap> layerNoiceList)
    {
        ClearLayer();
        this.OffsetY = offsetY;
        mapSizeX = WorldGeneration.Instance.mapSizeX;
        chunkTiles = new WorldTile[mapSizeX, chunkSizeY];

        GameObject[,] chunkPrefabTiles = new GameObject[mapSizeX, chunkSizeY];

        int lastSeed = seed;

        foreach (NoiceMap n in layerNoiceList)
        {
            System.Random prng = new System.Random(lastSeed);
            lastSeed = prng.Next();
            float[,] noiceMap = Noise.GenerateNoiseMap(mapSizeX, chunkSizeY, lastSeed, n.noiseScale, n.octaves, n.persistance, n.lacunarity, n.offset + (Vector2.down * offsetY));

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < chunkSizeY; y++)
                {
                    float amplitudeScale = Mathf.InverseLerp(0, 40 / 2, y+offsetY);
                    GameObject o = n.GetTilePrefab(Mathf.Clamp01(noiceMap[x, y] * amplitudeScale), (y + offsetY));
                    if(o != null)
                    {
                        chunkPrefabTiles[x, y] = o;
                    }
                }
            }
        }

        //Spawn Objects
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < chunkSizeY; y++)
            {
                Vector2 pos = new Vector2(x - (mapSizeX / 2), -(y + offsetY));
                GameObject prefab = chunkPrefabTiles[x,y];
                if(prefab == null)
                {
                    //spawn default
                    prefab = defaultPrefab;

                }
                GameObject newTile = Instantiate(prefab);
                newTile.transform.position = pos;
                newTile.transform.SetParent(this.gameObject.transform);
                chunkTiles[x, y] = newTile.GetComponent<WorldTile>();
            }
        }
    }

    [Serializable]
    public struct NoiceMap
    {
        public int octaves;// = 4;
        [Range(0, 1)]
        public float persistance;//; = 0.5f;
        public float lacunarity;// = 2.1f;
        public Vector2 offset;
        public float noiseScale;//; = 3f;
        public WorldGenTiles[] tiles;

        public GameObject GetTilePrefab(float noiceHeight, int depth)
        {
            GameObject tileObj = null;
            for (int i = 0; i < tiles.Length; i++)
            {
                float scale = 0;
                if (depth >= tiles[i].minDepth && depth <= tiles[i].bestDepthS)   //between min <-> best
                {
                    scale = Mathf.InverseLerp(tiles[i].minDepth-1, tiles[i].bestDepthS, depth); 
                }else if (depth >= tiles[i].bestDepthE && depth <= tiles[i].maxDepth) //between best <-> max
                {
                    scale = Mathf.InverseLerp(tiles[i].maxDepth, tiles[i].bestDepthE, depth);
                }
                else if (depth >= tiles[i].bestDepthS && depth <= tiles[i].bestDepthE) //between best <-> best
                {
                    scale = 1;
                }
                else
                {
                    scale = 0;                                                       // not between min <-> best <-> max dont spawn
                }
                float tileMaxHeight = tiles[i].noiceHeight * scale;
                //Debug.Log($"scale: {scale} + depth: {depth} noiceHeight:{noiceHeight} tileMaxHeight:{tileMaxHeight}");
                if (tileMaxHeight != 0 && noiceHeight <= tileMaxHeight)
                {
                    tileObj = tiles[i].tileObject;
                }
            }
            return tileObj;
        }
    }

    [Serializable]
    public struct WorldGenTiles
    {
        public string name;
        public int minDepth;
        public int bestDepthS;
        public int bestDepthE;
        public int maxDepth;
        public float noiceHeight;
        public GameObject tileObject;
    }
}
