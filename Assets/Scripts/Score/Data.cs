using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public int score;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static Data FromJson(string jsonData)
    {
        return JsonUtility.FromJson<Data>(jsonData);
    }
}