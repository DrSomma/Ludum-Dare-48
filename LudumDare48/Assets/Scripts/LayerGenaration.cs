using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerGenaration : MonoBehaviour
{
    public int mapSizeY = 20;
    protected int mapSizeX;

    public int octaves = 4;
    [Range(0, 1)]
    public float persistance = 0.5f;
    public float lacunarity = 2.1f;
    public Vector2 offset;
    public WorldTiles[] tiles;
    public SpecialTiles[] specialTiles;
    public float noiseScale = 3f;
    public WorldTile[,] layerMap;


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

    public virtual void GenerateLayer(int startY, int seed = -1)
    {
        ClearLayer();
        mapSizeX = WorldGeneration.Instance.mapSizeX;
        seed = seed == -1 ? UnityEngine.Random.Range(0, 1000) : seed;

        float[,] noiceMap = Noise.GenerateNoiseMap(mapSizeX, mapSizeY, seed, noiseScale, octaves, persistance, lacunarity, offset);
        layerMap = new WorldTile[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                GameObject tile = GetTile(noiceMap[x, y]);
                Vector2 pos = new Vector2(x - (mapSizeX / 2), -(y + startY));
                GameObject newTile = Instantiate(tile);
                newTile.transform.position = pos;
                newTile.transform.SetParent(this.gameObject.transform);
                layerMap[x, y] = newTile.GetComponent<WorldTile>();
            }
        }

        for (int i = 0; i < specialTiles.Length; i++)
        {
            int p = (int)UnityEngine.Random.Range(0, specialTiles[i].maxSpawn);
            for (int sp = 0; sp < p; sp++)
            {
                ReplaceRandomTile(specialTiles[i].tileObject.gameObject);
            }
        }
    }

    public GameObject GetTile(float height)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (height <= tiles[i].height)
            {
                return tiles[i].tileObject;
            }
        }
        Debug.LogError("NO TILE FOUND! " + height);
        return null;
    }

    public WorldTile GetRandomTile(out int x, out int y)
    {
        x = UnityEngine.Random.Range(0, mapSizeX);
        y = UnityEngine.Random.Range(0, mapSizeY);
        return layerMap[x, y];
    }

    protected void ReplaceRandomTile(GameObject replace)
    {
        GetRandomTile(out int x, out int y);
        ReplaceTile(replace, x, y);
    }

    protected void ReplaceTile(GameObject replace, int x, int y)
    {
        GameObject replaceTile = layerMap[x, y].gameObject;
        GameObject replaceBy = Instantiate(replace);
        replaceBy.transform.SetParent(this.transform);
        replaceBy.transform.position = replaceTile.transform.position;
        DestroyImmediate(replaceTile);
        layerMap[x, y] = replaceBy.GetComponent<WorldTile>();
    }

    [Serializable]
    public struct WorldTiles
    {
        public string name;
        public float height;
        public GameObject tileObject;
    }

    [Serializable]
    public struct SpecialTiles
    {
        public string name;
        public float spawnChance;
        public int maxSpawn;
        public GameObject tileObject;
    }
}
