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
    //public Vector3 heightMapScale = new Vector3(1, 1, 1);
    public float heightScale = 0f;
    public Texture2D heightMapImage;
    public bool resetTerrain = true;

    public float perlinMountainHi;
    public float perlinMountainLo;
    public int perlinTileSize;

    [System.Obsolete]
    void OnEnable()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        xSize = (int)terrainData.heightmapResolution;
        zSize = (int)terrainData.heightmapResolution;
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void SetHeightScale()
    {
        Debug.Log("Height Scale = " + heightScale);
    }

    float[,] GetHeightMap()
    {
        if (resetTerrain)
        {
            return terrainData.GetHeights(0, 0, xSize, zSize);
        }
        else
            return new float[xSize, zSize];
    }

    [System.Obsolete]
    public void ResetTerrain()
    {
        float[,] heightMap;
        heightMap = new float[xSize, zSize];
        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                heightMap[x, z] = 0;
                if (z == x)
                    heightMap[x, z] = .1f;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void PerlinTerrain()
    {
        float[,] heightMap;
        heightMap = new float[xSize, zSize];
        float hillHeight = (float)((float)perlinMountainHi - (float)perlinMountainLo) / ((float)terrain.terrainData.heightmapResolution / 2);
        float baseHeight = (float)5 / ((float)terrain.terrainData.heightmapResolution / 2);

        //heightMap = GetHeightMap();
        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                heightMap[x, z] = baseHeight + (Mathf.PerlinNoise(((float)x / (float)xSize) * perlinTileSize, ((float)z / (float)zSize) * perlinTileSize) * (float)hillHeight);
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void LoadTexture()
    {
        heightMap = new float[xSize, zSize];

        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                //heightMap[x, z] = heightMapImage.GetPixel((int)(x * heightMapScale.x), (int)(z * heightMapScale.z)).grayscale * heightMapScale.y;
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }
}
