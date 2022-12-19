using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FallOffGenerator
//page resource https://www.youtube.com/watch?v=COmtTyLCd6I&t=15s
{
    public static float[,] GenerateFallOffMap(int size)
    {
        float[,] map = new float[size,size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));

                map[i, j] = Evaluate(value);
            }
        }

        return map;
    }

    //remember equation f(x) = x^a^ over x^a^ + (b-bx)^a^ controls white and black in falloff 
    //side NOTE: a makes the main frequency and b helps to refine it and x is the given value
    static float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + (Mathf.Pow(b - b * value, a)));

    }

}
