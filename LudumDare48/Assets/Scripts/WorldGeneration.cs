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
    public GameObject player;
    public GameObject WeedPrefab;

    public int seed = 20594;

    private int _lastMapY = 0;
    private Queue<ChunkGenaration> chunkList;

    // Start is called before the first frame update
    void Start()
    {
        ClearMap();
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

    public void GenerateRndMap()
    {
        ClearMap();
        System.Random prng = new System.Random(seed);
        seed = prng.Next(0, 10000);
        GenerateMap();
    }

    public void GenerateMap()
    {
        _lastMapY = 0;
        chunkList = new Queue<ChunkGenaration>();

        GameObject grassline = new GameObject("weed");
        grassline.transform.SetParent(this.transform);

        for (int x = 0; x < mapSizeX; x++)
        {
            //spawn grass 
            var weed = Instantiate(WeedPrefab);
            weed.transform.position = new Vector2(x - (mapSizeX / 2), 1);
            weed.transform.SetParent(grassline.transform);
        }

        for (int i = 0; i < 3; i++)
        {
            AddChunk();
        }
    }

    private void AddChunk()
    {
        GameObject newLayerObject = Instantiate(chunkPrefab);
        newLayerObject.transform.SetParent(this.gameObject.transform);

        var gen = newLayerObject.GetComponent<ChunkGenaration>();
        gen.GenerateChunk(_lastMapY, seed, ores);
        _lastMapY += gen.chunkSizeY;
        chunkList.Enqueue(gen);
    }

    private void Update()
    {
        //check if we can destroy a chunk
        var first = chunkList.Peek();
        if (first != null)
        {
            if(player.transform.position.y < -(first.OffsetY + first.chunkSizeY*2))
            {
                chunkList.Dequeue();
                Destroy(first.gameObject);
                AddChunk();
            }

        }
    }


}