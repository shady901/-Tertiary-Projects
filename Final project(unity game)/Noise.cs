using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{//by Scott Korodi last update 11/10/2019
    public enum NormalizeMode {Local, Global};

    public static float[,] GeneratePNMap(int MWidth, int MHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset, NormalizeMode normalizeMode)
    {
        float[,] perlinMap = new float[MWidth, MHeight];

        //for use of same made maps(a seed)
        System.Random rannum = new System.Random(seed);
        Vector2[]octaveOffSets = new Vector2[octaves];

        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;


        for (int i = 0; i < octaves; i++)
        {
            float offsetX = rannum.Next(-100000, 100000) + offset.x;
            float offsetY = rannum.Next(-100000, 100000) - offset.y;
            octaveOffSets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude;
            amplitude *= persistance;
        }


        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        //part of return
        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        //to center the zoom in of the scale
        float halfWidth = MWidth/2f;
        float halfHeight = MHeight / 2f;



        for (int y = 0; y < MHeight; y++)
        {
            for (int x = 0; x < MWidth; x++)
            {
                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    //for the length and width of the map(high frequency = further sample points)
                    float sampleX = (x - halfWidth + octaveOffSets[i].x) / scale * frequency;
                    float sampleY = (y - halfHeight + octaveOffSets[i].y) / scale * frequency;

                    //NOTE: Mathf.perlinNoise returns a value between 0-1(to get map depth added code after sampleY)
                    float perlinVal = Mathf.PerlinNoise(sampleX, sampleY * 2 - 1);
                    //for the height(how tall) of the map
                    noiseHeight += perlinVal * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                //return noise validation
                if (noiseHeight > maxLocalNoiseHeight)
                {
                    maxLocalNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minLocalNoiseHeight)
                {
                    minLocalNoiseHeight = noiseHeight;
                }
                perlinMap[x, y] = noiseHeight;
            }
        }
        //Normalized noise map
        for (int y = 0; y < MHeight; y++)
        {
            for (int x = 0; x < MWidth; x++)
            {
                if (normalizeMode == NormalizeMode.Local)
                {
                    perlinMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, perlinMap[x, y]);
                }
                else
                {
                    float normalizedHeight = (perlinMap[x, y] + 1) / (2f * maxPossibleHeight / 1.15f);
                    perlinMap[x, y] = Mathf.Clamp(normalizedHeight, 0, 1);
                }
            }
        }
        return perlinMap;

    }
}
