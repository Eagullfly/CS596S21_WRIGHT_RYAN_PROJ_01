using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PGCTerrain))]
[CanEditMultipleObjects]
public class PGCTerrainEditor : Editor
{
    SerializedProperty heightMapImage;
    SerializedProperty heightScale;
    SerializedProperty resetTerrain;
    SerializedProperty perlinMountainHi;
    SerializedProperty perlinMountainLo;
    SerializedProperty perlinTileSize;

    bool showPGCValues = false;
    bool showPerlinValues = false;

    private void OnEnable()
    {
        heightMapImage = serializedObject.FindProperty("heightMapImage");
        heightScale = serializedObject.FindProperty("heightScale");
        resetTerrain = serializedObject.FindProperty("resetTerrain");
        perlinMountainHi = serializedObject.FindProperty("perlinMountainHi");
        perlinMountainLo = serializedObject.FindProperty("perlinMountainLo");
        perlinTileSize = serializedObject.FindProperty("perlinTileSize");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PGCTerrainEditor terrain = (PGCTerrain)target;

        EditorGUILayout.LabelField("PGC Terrain Editor", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(resetTerrain);

        showPGCValues = EditorGUILayout.Foldout(showPGCValues, "PGC Values");
        if (showPGCValues)
        {
            EditorGUILayout.LabelField("", OnInspectorGUI.skin.horizontalSlider);
            GUILayout.Label("Set PGC Values here ", EditorStyle.boldLabel);
            EditorGUILayout.PropertyField(heightScale);
            if(GUILayout.Button("Reset Terrain"))
            {
                terrain.resetTerrain();
            }
        }

        showPerlinValues = EditorGUILayout.Foldout(showPerlinValues, "Perlin Values");
        if (showPerlinValues)
        {
            GUILayout.Label("Set Perlin ", EditorStyles.boldLabel);
            EditorGUILayout.Slider(perlinMountainHi, 10, 600, new GUIContent("Top of Perlin Mountains"));
            EditorGUILayout.Slider(perlinMountainLo, -50, 9.8f, new GUIContent("Bottom of Perlin Mountains"));
            EditorGUILayout.IntSlider(perlinTileSize, 1, 129, new GUIContent("Terrain Tile Size"));
            if (GUILayout.Button("Add Perlin"))
            {
                terrain.PerlinTerrain();
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
