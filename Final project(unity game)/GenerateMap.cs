using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class GenerateMap : MonoBehaviour
{
    //determinds what gets drawn
    public enum DrawMode
    {
        NoiseMap, Mesh, FalloffMap, ColourMap
    }

    public float[,] spawnMap;

    public DrawMode drawMode;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public TextureData textureData;

    public Material terrainMaterial;

    public TerrainType[] regions;

    [Range(0, 6)]
    public int editPreviewLOD;

    //for update when inputting a size value
    public bool autoUpdate;

    //terrain Colours
    //public TerrainType[] regions;

    float[,] falloffMap;

    

    Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>>();
    Queue<MapThreadInfo<MeshData>> meshDataThreardInfoQueue = new Queue<MapThreadInfo<MeshData>>();

    //colours terrain based off perlin value
    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color Colour;
    }



    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            DrawMapInEditor();
        }
    }

    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);
    }

    public int mapChunkSize
    {
        get
        {
            if (terrainData.useFlatShading)
            {
                return 95;
            }
            else
            {
                return 239;
            }
        }
    }

    //displays map in a chosen form (mesh coloured or shaded(black and white))
    public void DrawMapInEditor()
    {
        MapData mapData = GenMapData(Vector2.zero);
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGen.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGen.TextureFromMapColour(mapData.colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGen.GenTerainMesh(mapData.heightMap, terrainData.meshHeightMulitplier, terrainData.meshHeightCurve, editPreviewLOD, terrainData.useFlatShading), TextureGen.TextureFromMapColour(mapData.colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGen.TextureFromHeightMap(FallOffGenerator.GenerateFallOffMap(mapChunkSize)));
        }

    }

    //test design
    public void RequestMapData(Vector2 centre,Action<MapData> callBack)
    {
        ThreadStart threadStart = delegate {MapDataThread(centre, callBack); };

        new Thread(threadStart).Start();
    }

    void MapDataThread(Vector2 centre, Action<MapData> callBack)
    {
        MapData mapData = GenMapData(centre);
        lock (mapDataThreadInfoQueue)
        {
            mapDataThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(callBack, mapData));
        }

    }

    //mesh map Design
    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callBack)
    {
        ThreadStart threadStart = delegate { MeshDataThread(mapData, lod, callBack); };
        new Thread(threadStart).Start();
    }

    void MeshDataThread(MapData mapData, int lod, Action<MeshData> callBack)
    {
        MeshData meshData = MeshGen.GenTerainMesh(mapData.heightMap, terrainData.meshHeightMulitplier, terrainData.meshHeightCurve, lod, terrainData.useFlatShading);
        lock (meshDataThreardInfoQueue)
        {
            meshDataThreardInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callBack, meshData));

        }
    }


    void Update()
    {
        if (mapDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < mapDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue();
                threadInfo.callBack(threadInfo.parameter);
            }
        }

        if (meshDataThreardInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshDataThreardInfoQueue.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshDataThreardInfoQueue.Dequeue();
                threadInfo.callBack(threadInfo.parameter);
            }
        }
    }



    //generats the map and generic data(size of map)
  public  MapData GenMapData(Vector2 centre)
    {
        float[,] noiseMap = Noise.GeneratePNMap(mapChunkSize + 2, mapChunkSize + 2, noiseData.seed, noiseData.pnScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, centre + noiseData.offset, noiseData.normalizeMode);
        spawnMap = noiseMap;
        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];

        if (terrainData.useFalloff)
        {

            if (falloffMap == null)
            {
                falloffMap = FallOffGenerator.GenerateFallOffMap(mapChunkSize + 2);
            }
            
            //for map colours
            //Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
            for (int y = 0; y < mapChunkSize + 2; y++)
            {   
                for (int x = 0; x < mapChunkSize + 2; x++)
                {
                    if (terrainData.useFalloff)
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    }
                    float currentHeight = noiseMap[x, y];
                    for (int i = 0; i < regions.Length; i++)
                    {
                        if (currentHeight >= regions[i].height)
                        {
                            colourMap[y * mapChunkSize + x] = regions[i].Colour;
                        }
                        else { break; }
                    }
                   


                }
            }
        }
        
        return new MapData(noiseMap, colourMap);

    }

    //constrains map variable sizes
    void OnValidate()
    {
        if (terrainData != null)
        {
            terrainData.OnValuesUpdated -= OnValuesUpdated;
            terrainData.OnValuesUpdated += OnValuesUpdated;
        }
        if (noiseData != null)
        {
            noiseData.OnValuesUpdated -= OnValuesUpdated;
            noiseData.OnValuesUpdated += OnValuesUpdated;
        }
        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }

    }

    struct MapThreadInfo<T>
    {
        public readonly Action<T> callBack;
        public readonly T parameter;

        public MapThreadInfo(Action<T> callBack, T parameter)
        {
            this.callBack = callBack;
            this.parameter = parameter;
        }
    }



}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}


public struct MapData
{
    public readonly float[,] heightMap;
    public readonly Color[] colourMap;

    public MapData(float[,] heightMap, Color[] colourMap)
    {
        this.heightMap = heightMap;
        this.colourMap = colourMap;
    }
}
