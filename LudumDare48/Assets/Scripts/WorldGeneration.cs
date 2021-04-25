using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGeneration : MonoBehaviour
{
    #region SINGLETON PATTERN

    private static WorldGeneration _instance;

    public static WorldGeneration Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WorldGeneration>();

                if (_instance == null)
                {
                    GameObject container = new GameObject(name: "WorldGeneration");
                    _instance = container.AddComponent<WorldGeneration>();
                }
            }

            return _instance;
        }
    }

    #endregion
    
    public int mapMaxX => mapSizeX/2;
    public int mapMinX => -mapSizeX/2;

    public int mapSizeX = 20;
    public int mapSizeY = 40;
    public int octaves = 4;
    [Range(0,1)]
    public float persistance = 0.5f;
    public float lacunarity = 2.1f;
    public int seed = 20594;
    public Vector2 offset;

    public WorldTiles[] tiles;

    public float noiseScale = 3f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    public void ClearMap()
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

    public void GenerateMap()
    {
        ClearMap();

        float[,] noiceMap = Noise.GenerateNoiseMap(mapSizeX, mapSizeY, seed, noiseScale, octaves, persistance, lacunarity, offset);
        //_map = new WorldTile[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                GameObject tile = GetRandomTile(noiceMap[x,y]);
                Vector2 pos = new Vector2(x - (mapSizeX / 2), -y);
                GameObject newTile = Instantiate(tile);
                newTile.transform.position = pos;
                newTile.transform.SetParent(this.gameObject.transform);
                //_map[x, y] = newTile.GetComponent<WorldTile>();
            }
        }
    }

    public GameObject GetRandomTile(float height)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if(height <= tiles[i].height)
            {
                return tiles[i].tileObject ;
            }
        }
        Debug.LogError("NO TILE FOUND! " + height);
        return null;
    }

    private void OnValidate()
    {
        if(mapSizeX < 1)
        {
            mapSizeX = 1;
        }
        if(mapSizeY < 1)
        {
            mapSizeY = 1;
        }
    }


    //private WorldTile[,] _map;
    //public WorldTile GetTile(float xPos, float yPos)
    //{
    //    int arrayX = (int)(xPos + (mapSizeX / 2));
    //    //Debug.Log($"GetTile: {xPos} {arrayX}");
    //    if (arrayX < 0 || arrayX >= mapSizeX)
    //        return null;
    //    if (yPos < ((mapSizeY-1) * -1f) || yPos > 0)
    //        return null;
    //    int arrayY = (int)yPos * -1;
    //    return _map[arrayX, arrayY];
    //}
    //public void DeleteTile(float xPos, float yPos)
    //{
    //    WorldTile tile = GetTile(xPos, yPos);
    //    if(tile != null)
    //    {
    //        Destroy(tile.gameObject);
    //    }
    //}

    [Serializable]
    public struct WorldTiles
    {
        public string name;
        public float height;
        public GameObject tileObject;
    }
}
