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

    public float trigFuncHi;
    public float trigFuncLo;
    public float trigTileSize;
    public float amplitude;
    public float wavelength;

    public float randomHeightRange;
    public float randomHeightTiles;

    public float perlinMountainHi;
    public float perlinMountainLo;
    public int perlinTileSize;
    

    public float voronoiMountainHeight;
    
    public int roughness;

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
    public void TrigTerrain()
    {
        float[,] heightMap;
        heightMap = new float[xSize, zSize];
        float hillHeight = (float)((float)trigFuncHi - (float)trigFuncLo)/ ((float)terrain.terrainData.heightmapResolution / 2);
        float baseHeight = (float)5 / ((float)terrain.terrainData.heightmapResolution / 2);

        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                heightMap[x, z] = baseHeight + (Mathf.Sin(((float)x * Mathf.PI / (float)xSize) * trigTileSize)) * (Mathf.Cos(((float)z * Mathf.PI / (float)zSize) * trigTileSize) * (float)hillHeight);
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void RandHeightTerrain()
    {
        float[,] heightMap;
        heightMap = new float[xSize, zSize];
        float hillHeight = (float)randomHeightRange / ((float)terrain.terrainData.heightmapResolution / 2);
        float baseHeight = (float)5 / ((float)terrain.terrainData.heightmapResolution / 2);
        for (int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                heightMap[x, z] = baseHeight + (Mathf.Sin(((float)x/(float)xSize)*randomHeightTiles)) * (Mathf.Cos(((float)z/(float)zSize)*randomHeightTiles)) * (float)hillHeight;
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

        
        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                heightMap[x, z] = baseHeight + (Mathf.PerlinNoise(((float)x / (float)xSize) * perlinTileSize, ((float)z / (float)zSize) * perlinTileSize) * (float)hillHeight);
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void MultPerlinTerrain()
    {
        float[,] heightMap;
        
        heightMap = new float[xSize, zSize];
        float hillHeight = (float)((float)perlinMountainHi - (float)perlinMountainLo) / ((float)terrain.terrainData.heightmapResolution / 2);
        float baseHeight = (float)5 / ((float)terrain.terrainData.heightmapResolution / 2);
        for (int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                heightMap[x, z] = (baseHeight + (Mathf.PerlinNoise(((float)x / (float)xSize) * perlinTileSize, ((float)z / (float)zSize) * perlinTileSize) * (float)hillHeight));
                for (int i = 0; i < 4; i++)
                {
                    heightMap[x, z] += (baseHeight + (Mathf.PerlinNoise(Mathf.Pow((((float)x / (float)xSize) * perlinTileSize), i), Mathf.Pow(((float)z / (float)zSize) * perlinTileSize,i)) / (1 / Mathf.Pow(2, i))) * (float)hillHeight);
                }
                heightMap[x, z] /= 60;
                
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void VoronoiTerrain()
    {
        float[,] heightMap;
        heightMap = new float[xSize, zSize];
        float hillHeight = voronoiMountainHeight / (terrain.terrainData.heightmapResolution / 2);
        float baseHeight = 5 / (terrain.terrainData.heightmapResolution / 2);
        for (int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                heightMap[x, z] =  baseHeight + (Random.value * hillHeight);
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void MidDisSoothTerrain()
    {
        float[,] heightMap;
        heightMap = new float[xSize, zSize];
        
        float range =  0.5f * roughness / ((int)terrain.terrainData.heightmapResolution / 2);
        float average;
        for (int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                average = heightMap[x, z];
                average += (Random.value * (range * 2.0f)) - range;
                heightMap[x, z] = average;
                    
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    
}
