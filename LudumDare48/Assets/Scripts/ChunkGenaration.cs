using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenaration : MonoBehaviour
{
    public int chunkSizeY = 20;
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
        mapSizeX = WorldGeneration.Instance.mapSizeX;
        chunkTiles = new WorldTile[mapSizeX, chunkSizeY];

        GameObject[,] chunkPrefabTiles = new GameObject[mapSizeX, chunkSizeY];

        foreach (NoiceMap n in layerNoiceList)
        {
            float[,] noiceMap = Noise.GenerateNoiseMap(mapSizeX, chunkSizeY, seed, n.noiseScale, n.octaves, n.persistance, n.lacunarity, n.offset + (Vector2.down * offsetY));

            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < chunkSizeY; y++)
                {
                    GameObject o = n.GetTilePrefab(noiceMap[x, y], (y + offsetY));
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
                if(prefab != null)
                {
                    GameObject newTile = Instantiate(prefab);
                    newTile.transform.position = pos;
                    newTile.transform.SetParent(this.gameObject.transform);
                    chunkTiles[x, y] = newTile.GetComponent<WorldTile>();
                }
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
            GameObject tileObject = null;
            for (int i = 0; i < tiles.Length; i++)
            {
                if (noiceHeight >= tiles[i].noiceHeight && depth >= tiles[i].minDepth)
                {
                    tileObject = tiles[i].tileObject;
                }
            }
            Debug.LogError("NO TILE FOUND! " + noiceHeight);
            return tileObject;
        }
    }

    [Serializable]
    public struct WorldGenTiles
    {
        public string name;
        public int minDepth;
        public float noiceHeight;
        public GameObject tileObject;
    }
}
