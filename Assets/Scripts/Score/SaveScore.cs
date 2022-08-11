using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveScore : MonoBehaviour
{
    public static SaveScore instance;
    public int numberLevel;
    public Data levelData;
    public event Action<Data> OnLoadLevelData;
    string _fileName = "Level{0}Data.sav";
    string _savePath;

    private void Awake()
    {
        instance = this;
        _savePath = Application.persistentDataPath + "/" + string.Format(_fileName, numberLevel);
    }

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        try
        {
            if (!File.Exists(_savePath))
                File.Create(_savePath);

            StreamWriter streamWriter = new StreamWriter(_savePath, false);
            streamWriter.Write(levelData.ToJson());
            streamWriter.Close();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void Load()
    {
        try
        {
            if (levelData == null)
                levelData = new Data();

            if (File.Exists(_savePath))
            {
                StreamReader streamReader = new StreamReader(_savePath);
                levelData = Data.FromJson(streamReader.ReadToEnd());
                streamReader.Close();

                OnLoadLevelData?.Invoke(levelData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
