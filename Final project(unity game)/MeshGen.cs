using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGen
{//by Scott Korodi




    public static MeshData GenTerainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve _heightCurve, int lvlOfDetail, bool useFlatShading)
    {
        AnimationCurve heightCurve = new AnimationCurve(_heightCurve.keys);

        int meshSimplificationIncreament = (lvlOfDetail == 0) ? 1 : lvlOfDetail * 2;

        int borderedSize = heightMap.GetLength(0);
        int meshSize = borderedSize - 2 * meshSimplificationIncreament;
        int meshSizeUnsimplified = borderedSize - 2;

        float topLeftx = meshSizeUnsimplified - 1/-2f;
        float topLeftz = meshSizeUnsimplified - 1 / 2f;
        //NOTE the = end of this is another way to do if staments if =()? then = val:val  the : is like an else
        
        int verticesPerLine = (meshSize - 1) / meshSimplificationIncreament + 1;

        MeshData meshData = new MeshData(borderedSize, useFlatShading);

        int[,] verticesIndicesMap = new int[borderedSize, borderedSize];
        int meshVertexIndex = 0;
        int borderVertexIndex = -1;

        for (int y = 0; y < borderedSize; y += meshSimplificationIncreament)
        {
            for (int x = 0; x < borderedSize; x += meshSimplificationIncreament)
            {
                bool isBorderVertex = y == 0 || y == borderedSize - 1 || x == 0 || x == borderedSize - 1;

                if (isBorderVertex)
                {
                    verticesIndicesMap[x, y] = borderVertexIndex;
                    borderVertexIndex--;
                }
                else
                {
                    verticesIndicesMap[x, y] = meshVertexIndex;
                    meshVertexIndex++;
                }
            }
        }


        for (int y = 0; y < borderedSize; y+= meshSimplificationIncreament)
        {
            for (int x = 0; x < borderedSize; x+= meshSimplificationIncreament)
            {
                int vertexIndex = verticesIndicesMap[x, y];

                Vector2 percent = new Vector2((x - meshSimplificationIncreament) / (float)meshSize, (y - meshSimplificationIncreament) / (float)meshSize);
                float height = heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier;

                Vector3 vertexPosition = new Vector3(topLeftx + percent.x * meshSizeUnsimplified, height, topLeftz - percent.y * meshSizeUnsimplified);

                meshData.AddVertex(vertexPosition, percent, vertexIndex);


                if (x < borderedSize - 1 && y < borderedSize -1)
                {
                    int a = verticesIndicesMap[x, y];
                    int b = verticesIndicesMap[x + meshSimplificationIncreament, y];
                    int c = verticesIndicesMap[x, y + meshSimplificationIncreament];
                    int d = verticesIndicesMap[x + meshSimplificationIncreament, y + meshSimplificationIncreament];


                    meshData.AddTriangle(a,d,c);//first triangle
                    meshData.AddTriangle(d,a,b);//seacond triangle
                }

                vertexIndex++;
            }
        }

        meshData.ProcessMesh();

        return meshData;
    }
}

public class MeshData
{
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    Vector3[] bakedNormals;

    Vector3[] borderVertices;
    int[] borderTriangles;

    int borderTriangleIndex;
    int triangleindex;

    bool useFlatShading;

    public MeshData(int verticesPerLine, bool useFlatShading)
    {
        this.useFlatShading = useFlatShading;

        vertices = new Vector3[verticesPerLine * verticesPerLine];
        uvs = new Vector2[verticesPerLine * verticesPerLine];
        triangles = new int[(verticesPerLine - 1) * (verticesPerLine - 1) * 6];

        borderVertices = new Vector3[verticesPerLine * 4 + 4];
        borderTriangles = new int[24 * verticesPerLine];

    }

    public void AddVertex(Vector3 vertexPostion, Vector2 uv, int vertexIndex)
    {
        if (vertexIndex < 0)
        {
            borderVertices[-vertexIndex - 1] = vertexPostion;
        }
        else
        {
            vertices[vertexIndex] = vertexPostion;
            uvs[vertexIndex] = uv;
        }
    }


    public void AddTriangle(int a, int b, int c)
    {
        if (a < 0 || b < 0 || c < 0)
        {
            borderTriangles[borderTriangleIndex] = a;
            borderTriangles[borderTriangleIndex + 1] = b;
            borderTriangles[borderTriangleIndex + 2] = c;
            borderTriangleIndex += 3;
        }
        else
        {
            triangles[triangleindex] = a;
            triangles[triangleindex + 1] = b;
            triangles[triangleindex + 2] = c;
            triangleindex += 3;
        }

    }

    Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3[vertices.Length];
        int trianglesCount = triangles.Length / 3;

        for (int i = 0; i < trianglesCount; i++)
        {
            int normalTriangleIndex = i * 3;

            int vertexIndexA = triangles[normalTriangleIndex];
            int vertexIndexB = triangles[normalTriangleIndex + 1];
            int vertexIndexC = triangles[normalTriangleIndex + 2];

            Vector3 traingleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);

            vertexNormals[vertexIndexA] += traingleNormal;
            vertexNormals[vertexIndexB] += traingleNormal;
            vertexNormals[vertexIndexC] += traingleNormal;
        }

        int borderTrianglesCount = borderTriangles.Length / 3;

        for (int i = 0; i < borderTrianglesCount; i++)
        {
            int normalTriangleIndex = i * 3;

            int vertexIndexA = borderTriangles[normalTriangleIndex];
            int vertexIndexB = borderTriangles[normalTriangleIndex + 1];
            int vertexIndexC = borderTriangles[normalTriangleIndex + 2];

            Vector3 traingleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);

            if (vertexIndexA >= 0)
            {
                vertexNormals[vertexIndexA] += traingleNormal;
            }
            if (vertexIndexB >= 0)
            {
                vertexNormals[vertexIndexB] += traingleNormal;
            }
            if (vertexIndexC >= 0)
            {
                vertexNormals[vertexIndexC] += traingleNormal;
            }
        }




        for (int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }

    Vector3 SurfaceNormalFromIndices(int indexA, int indexB, int indexC)
    {
        Vector3 pointA = (indexA < 0)?borderVertices[-indexA-1] : vertices[indexA];
        Vector3 pointB = (indexB < 0)?borderVertices[-indexB-1] : vertices[indexB];
        Vector3 pointC = (indexC < 0)?borderVertices[-indexC-1] : vertices[indexC];

        //Note: Cross product and dot product equations
        Vector3 sideAB = pointB - pointA;
        Vector3 sideAC = pointC - pointA;
        return Vector3.Cross(sideAB, sideAC).normalized;

    }

    public void ProcessMesh()
    {
        if (useFlatShading)
        {
            FlatShading();
        }
        else
        {
            BakeNormals();
        }
    }

    void BakeNormals()
    {
        bakedNormals = CalculateNormals();
    }

    
    void FlatShading()
    {
        Vector3[] flatShadedVertices = new Vector3[triangles.Length];
        Vector2[] flatshadedUvs = new Vector2[triangles.Length];

        for (int i = 0; i < triangles.Length; i++)
        {
            flatShadedVertices[i] = vertices[triangles[i]];
            flatshadedUvs[i] = uvs[triangles[i]];
            triangles[i] = i;
        }

        vertices = flatShadedVertices;
        uvs = flatshadedUvs;


    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        if (useFlatShading)
        {
            mesh.RecalculateNormals();
        }
        else
        {
            mesh.normals = bakedNormals;
        }
        


        //mesh.RecalculateNormals();//for lighting
        return mesh;
    }


}
