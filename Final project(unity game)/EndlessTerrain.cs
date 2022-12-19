using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    

    //rendering
    const float viewerMoveMaxChunkRate = 25f;
    const float sqrViewerMoveMaxChunkRate = viewerMoveMaxChunkRate * viewerMoveMaxChunkRate;

    //render distance
    public LODInfo[] detailLevels;
    public static float maxViewDst;

    //test player and map
    public Transform viewer;
    public Material mapMaterial;

    //map stats
    public static Vector2 viewerPosition;
    Vector2 viewPositionOld;
    static GenerateMap mapGenerator;
    int chunkSize;
    int chunksViewableInViewDst;

    Dictionary<Vector2, TerrainChunk> terrainChunkDict = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> terrainChunksLastVisableUpdate = new List<TerrainChunk>();

    //establish player view
    void Start()
    {
        mapGenerator = FindObjectOfType<GenerateMap>();

        maxViewDst = detailLevels[detailLevels.Length - 1].visibleDstThreshold;
        chunkSize = mapGenerator.mapChunkSize - 1;
        chunksViewableInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);

        updateVisableChunks();
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z) /  mapGenerator.terrainData.uniformScale;

        if ((viewPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveMaxChunkRate)
        {
            viewPositionOld = viewerPosition;
            updateVisableChunks();
        }

    }


    //viewable Chunks
    void updateVisableChunks()
    {

        for (int i = 0; i < terrainChunksLastVisableUpdate.Count; i++)
        {
            terrainChunksLastVisableUpdate[i].SetVisable(false);
        }
        terrainChunksLastVisableUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffSet = -chunksViewableInViewDst; yOffSet <= chunksViewableInViewDst; yOffSet++)
        {
            for (int xOffSet = -chunksViewableInViewDst; xOffSet <= chunksViewableInViewDst; xOffSet++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffSet, currentChunkCoordY + yOffSet);

                if (terrainChunkDict.ContainsKey(viewedChunkCoord))
                {
                    terrainChunkDict[viewedChunkCoord].UpdateTerrain();
                }
                else
                {
                    terrainChunkDict.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, detailLevels, transform, mapMaterial));
                }
            }
        }
    }


    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        MapData mapData;
        bool mapDataReceived;
        int previousLODIndex = -1;


        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        MeshCollider meshCollider;

        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;
        LODMesh collisionLODMesh;

        

        //generates terrain
        public TerrainChunk(Vector2 coord, int size, LODInfo[] detailLevels, Transform parent, Material material)
        {
            this.detailLevels = detailLevels;

            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshCollider = meshObject.AddComponent<MeshCollider>();
            meshRenderer.material = material;


            meshObject.transform.position = positionV3 * mapGenerator.terrainData.uniformScale;
            meshObject.transform.parent = parent;
            meshObject.transform.localScale = Vector3.one * mapGenerator.terrainData.uniformScale;

            SetVisable(false);

            lodMeshes = new LODMesh[detailLevels.Length];
            for (int i = 0; i < detailLevels.Length; i++)
            {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateTerrain);
                if (detailLevels[i].useForCollider)
                {
                    collisionLODMesh = lodMeshes[i];
                }
            }


            mapGenerator.RequestMapData(position, OnMapDataReceived);


        }

        

        void OnMapDataReceived(MapData mapData)
        {
            this.mapData = mapData;
            mapDataReceived = true;
            //Texture2D texture = new TextureGen.TextureFromMapColour(mapData.colourMap, mapGenerator.mapChunkSize, mapGenerator.mapChunkSize);
            
            //meshRenderer.material.mainTexture = texture;


            UpdateTerrain();
            // apart of test mapGenerator.RequestMeshData(mapData, OnMeshDataReceived);
        }

        //testing part of mesh loading
        //void OnMeshDataReceived(MeshData meshData)
        //{
        //    meshFilter.mesh = meshData.CreateMesh();
        //}

        public void UpdateTerrain()
        {
            if (mapDataReceived)
            {
                float viewerDstFromNearEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
                bool visable = viewerDstFromNearEdge <= maxViewDst;

                if (visable)
                {
                    int lodIndex = 0;

                    for (int i = 0; i < detailLevels.Length - 1; i++)
                    {
                        if (viewerDstFromNearEdge > detailLevels[i].visibleDstThreshold)
                        {
                            lodIndex = i + 1;
                        }
                        else { break; }
                    }

                    if (lodIndex != previousLODIndex)
                    {
                        LODMesh lodMesh = lodMeshes[lodIndex];
                        if (lodMesh.hasMesh)
                        {
                            previousLODIndex = lodIndex;
                            meshFilter.mesh = lodMesh.mesh;
                        }
                        else if (!lodMesh.hasRequestedMesh)
                        {
                            lodMesh.RequestMesh(mapData);
                        }
                    }

                    if (lodIndex == 0)
                    {
                        meshCollider.sharedMesh = collisionLODMesh.mesh;
                    }
                    else if (!collisionLODMesh.hasRequestedMesh)
                    {
                        collisionLODMesh.RequestMesh(mapData);
                    }

                    terrainChunksLastVisableUpdate.Add(this);

                }

                SetVisable(visable);
            }
        }

        public void SetVisable(bool visable)
        {
            meshObject.SetActive(visable);
        }

        public bool IsVisable()
        {
            return meshObject.activeSelf;
        }
    }

    class LODMesh
    {
        public Mesh mesh;
        public bool hasRequestedMesh;
        public bool hasMesh;
        int lod;

        System.Action updateCallBack;

        public LODMesh(int lod, System.Action updateCallBack)
        {
            this.lod = lod;
            this.updateCallBack = updateCallBack;
        }

        void onMeshDataReceived(MeshData meshData)
        {
            mesh = meshData.CreateMesh();
            hasMesh = true;

            updateCallBack();
        }

        public void RequestMesh(MapData mapData)
        {
            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, onMeshDataReceived);
        }


    }

    //rendering distance
    [System.Serializable]//shows up in inspector
    public struct LODInfo
    {
        public int lod;
        public float visibleDstThreshold;
        public bool useForCollider;
    }

}
