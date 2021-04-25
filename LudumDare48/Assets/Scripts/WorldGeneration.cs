using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChunkGenaration;
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

    public int mapMaxX => mapSizeX / 2;
    public int mapMinX => -mapSizeX / 2;

    public int mapSizeX = 20;
    public GameObject chunkPrefab;
    public List<NoiceMap> ores;


    public int seed = 20594;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    private void OnValidate()
    {
        if (mapSizeX < 1)
        {
            mapSizeX = 1;
        }
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
        int lastMapY = 0;
        for (int i = 0; i < 3; i++)
        {
            GameObject newLayerObject = Instantiate(chunkPrefab);
            newLayerObject.transform.SetParent(this.gameObject.transform);
         
            newLayerObject.GetComponent<ChunkGenaration>().GenerateChunk(lastMapY, seed, ores);
            lastMapY += newLayerObject.GetComponent<ChunkGenaration>().chunkSizeY;
        }
    }


}