using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PGCTerrain : MonoBehaviour
{

    public Terrain terrain;
    public TerrainData terrainData;
    private float[,] heightMap;
    int xSize, zSize;

    public float heightScale = 0f;
    public Texture2D heightMapImage;
    public bool resetTerrain = true;

    public float perlinMountainHi;
    public float perlinMountainLo;
    public int perlinTileSize;

    [System.Obsolete]
    void OnEnable()
    {
        terrain = GetComponent<terrain>();
        terrainData = terrain.terrainData;
        xSize = (int)terrain.terrainData.size.x;
        zSize = (int)terrainData.size.z;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHeightScale()
    {
        Debug.Log("Height Scale = " + heightScale);
    }
}
