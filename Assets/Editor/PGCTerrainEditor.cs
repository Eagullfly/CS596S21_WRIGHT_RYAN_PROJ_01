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
    //SerializedProperty amplitude;
    //SerializedProperty wavelength;
    SerializedProperty randomHeightRange;
    SerializedProperty randomHeightTiles;
    SerializedProperty trigFuncHi;
    SerializedProperty trigFuncLo;
    SerializedProperty trigTileSize;
    SerializedProperty numberOfPerlin;
    //SerializedProperty leftHeight;
    //SerializedProperty rightHeight;
    SerializedProperty roughness;
    //SerializedProperty voronoiPoints;
    SerializedProperty voronoiMountainHeight;

    bool showPGCValues = false;
    bool showTrigFuncValues = false;
    bool showRandomHeights = false;
    bool showPerlinValues = false;
    bool showMultiplePerlin = false;
    bool showVoronoiValues = false;
    bool showMidpointDispSooth = false;

    private void OnEnable()
    {
        heightMapImage = serializedObject.FindProperty("heightMapImage");
        heightScale = serializedObject.FindProperty("heightScale");
        resetTerrain = serializedObject.FindProperty("resetTerrain");
        perlinMountainHi = serializedObject.FindProperty("perlinMountainHi");
        perlinMountainLo = serializedObject.FindProperty("perlinMountainLo");
        perlinTileSize = serializedObject.FindProperty("perlinTileSize");
        //amplitude = serializedObject.FindProperty("amplitude");
        //wavelength = serializedObject.FindProperty("wavelength");
        trigFuncHi = serializedObject.FindProperty("trigFuncHi");
        trigFuncLo = serializedObject.FindProperty("trigFuncLo");
        trigTileSize = serializedObject.FindProperty("trigTileSize");
        randomHeightRange = serializedObject.FindProperty("randomHeightRange");
        randomHeightTiles = serializedObject.FindProperty("randomHeightTiles");
        numberOfPerlin = serializedObject.FindProperty("numberOfPerlin");
        //leftHeight = serializedObject.FindProperty("leftHeight");
        //rightHeight = serializedObject.FindProperty("rightHeight");
        roughness = serializedObject.FindProperty("roughness");
       // voronoiPoints = serializedObject.FindProperty("voronoiPoints");
        voronoiMountainHeight = serializedObject.FindProperty("voronoiMountainHeight");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PGCTerrain terrain = (PGCTerrain)target;

        EditorGUILayout.LabelField("PGC Terrain Editor", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(resetTerrain);

        //showPGCValues = EditorGUILayout.Foldout(showPGCValues, "PGC Values");
        //if (showPGCValues)
        //{
            //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            //GUILayout.Label("Set PGC Values here ", EditorStyles.boldLabel);
            //EditorGUILayout.PropertyField(heightScale);
            if(GUILayout.Button("Reset Terrain"))
            {
                terrain.ResetTerrain();
            }
        //}
        showTrigFuncValues = EditorGUILayout.Foldout(showTrigFuncValues, "Trig Function Values");
        if (showTrigFuncValues)
        {
            GUILayout.Label("Set Trig Function ", EditorStyles.boldLabel);
            //EditorGUILayout.Slider(amplitude, 10, 600, new GUIContent("amplitude"));
            //EditorGUILayout.Slider(wavelength, -50, 9.8f, new GUIContent("wavelength"));
            EditorGUILayout.Slider(trigFuncHi, -50, 50, new GUIContent("trig Func Hi"));
            EditorGUILayout.Slider(trigFuncLo, -50, 50, new GUIContent("trig Func Lo"));
            EditorGUILayout.Slider(trigTileSize, 1, 129, new GUIContent("Terrain Tile Size"));
            if(GUILayout.Button("Add trig Function"))
            {
                terrain.TrigTerrain();
            }
        }

        showRandomHeights = EditorGUILayout.Foldout(showRandomHeights, "Random Heights Values");
        if (showRandomHeights)
        {
            GUILayout.Label("Set Height ", EditorStyles.boldLabel);
            EditorGUILayout.Slider(randomHeightRange, 0, 100);
            EditorGUILayout.Slider(randomHeightTiles, 1, 129, new GUIContent("Terrain Tile Size"));
            if (GUILayout.Button("Add Random Height"))
            {
                terrain.RandHeightTerrain();
            }
        }

        showPerlinValues = EditorGUILayout.Foldout(showPerlinValues, "Perlin Values");
        if (showPerlinValues)
        {
            //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Set Perlin ", EditorStyles.boldLabel);
            //EditorGUILayout.PropertyField(heightScale);
            EditorGUILayout.Slider(perlinMountainHi, 10, 600, new GUIContent("Top of Perlin Mountains"));
            EditorGUILayout.Slider(perlinMountainLo, -50, 9.8f, new GUIContent("Bottom of Perlin Mountains"));
            EditorGUILayout.IntSlider(perlinTileSize, 1, 129, new GUIContent("Terrain Tile Size"));
            if (GUILayout.Button("Add Perlin"))
            {
                terrain.PerlinTerrain();
            }
        }
        showMultiplePerlin = EditorGUILayout.Foldout(showMultiplePerlin, "Multiple Perlin Values");
        if (showMultiplePerlin)
        {
            GUILayout.Label("Set Multiple Perlin ", EditorStyles.boldLabel);

            EditorGUILayout.Slider(perlinMountainHi, 10, 600, new GUIContent("Top of Perlin Mountains"));
            EditorGUILayout.Slider(perlinMountainLo, -50, 9.8f, new GUIContent("Bottom of Perlin Mountains"));
            EditorGUILayout.IntSlider(perlinTileSize, 1, 129, new GUIContent("Terrain Tile Size"));
            EditorGUILayout.IntSlider(numberOfPerlin, 1, 100, new GUIContent("# of Perlin"));
            if (GUILayout.Button("Add Multiple Perlin"))
            {
                terrain.MultPerlinTerrain();
            }
        }

        showVoronoiValues = EditorGUILayout.Foldout(showVoronoiValues, "Voronoi Values");
        if (showVoronoiValues)
        {
            GUILayout.Label("Set Voronoi ", EditorStyles.boldLabel);
            EditorGUILayout.Slider(voronoiMountainHeight,0,100,new GUIContent("Mountain Height"));
            //EditorGUILayout.IntSlider(voronoiPoints, 0, 100, new GUIContent("Voronoi Points"));
            if(GUILayout.Button("Add Voronoi"))
            {
                terrain.VoronoiTerrain();
            }
        }

        showMidpointDispSooth = EditorGUILayout.Foldout(showMidpointDispSooth, "Midpoint Displacement Soothing Values");
        if (showMidpointDispSooth)
        {
            GUILayout.Label("Set Displacement Soothing", EditorStyles.boldLabel);
            //EditorGUILayout.IntSlider(leftHeight, 0, 100, new GUIContent("Left Height"));
            //EditorGUILayout.IntSlider(rightHeight, 0, 100, new GUIContent("Right Height"));
            EditorGUILayout.IntSlider(roughness, 0, 100, new GUIContent("Roughness"));
            if(GUILayout.Button("Add Displacement Soothing"))
            {
                terrain.MidDisSoothTerrain();
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
