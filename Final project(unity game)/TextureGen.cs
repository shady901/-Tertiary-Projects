using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGen 
{
    public static Texture2D TextureFromMapColour(Color[] mapColour, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixels(mapColour);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Color[] mapColour = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                mapColour[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);

            }
        }
        return TextureFromMapColour(mapColour, width, height);
    }

}
