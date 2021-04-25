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

    public int mapMaxX => mapSizeX / 2;
    public int mapMinX => -mapSizeX / 2;

    public int mapSizeX = 20;
    public int mapSizeY = 40;

    public Layer[] AllLayer;


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
        if (mapSizeY < 1)
        {
            mapSizeY = 1;
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
        for (int i = 0; i < AllLayer.Length; i++)
        {
            Layer layer = AllLayer[i];
            GameObject newLayerObject = Instantiate(layer.layerGen.gameObject);
            newLayerObject.transform.SetParent(this.gameObject.transform);
            newLayerObject.GetComponent<LayerGenaration>().GenerateLayer(seed + i);
            //layer.layerGen.GenerateLayer(seed + i);
        }
    }

    [Serializable]
    public struct Layer
    {
        public string name;
        public float depth;
        public LayerGenaration layerGen;
    }


}