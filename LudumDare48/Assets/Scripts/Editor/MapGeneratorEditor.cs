using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldGeneration))]
public class MapGeneratorEditor : Editor
{
    public bool autoUpdate = true;

    public override void OnInspectorGUI()
    {
        WorldGeneration worldGen = (WorldGeneration)target;
        bool hasUpdate = DrawDefaultInspector();

        autoUpdate = EditorGUILayout.Toggle("Auto Update", autoUpdate);
        if (autoUpdate && hasUpdate)
        {
            worldGen.GenerateMap();
        }


        if (GUILayout.Button("Generate"))
        {
            worldGen.ClearMap();
            worldGen.GenerateMap();
        }
        if (GUILayout.Button("Rnd Seed"))
        {
            worldGen.seed = UnityEngine.Random.Range(0, 1000);
            worldGen.ClearMap();
            worldGen.GenerateMap();
        }
        if (GUILayout.Button("Clear Map"))
        {
            worldGen.ClearMap();
        }

    }
}
