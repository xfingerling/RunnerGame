using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    public static SaveManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new SaveManager();
            return _instance;
        }
    }
    private static SaveManager _instance;

    private const string SAVE_FILE_NAME = "SaveData.ss";

    public SaveData save;

    private BinaryFormatter _formatter;

    public SaveManager()
    {
        _formatter = new BinaryFormatter();

        Load();
    }

    public void Load()
    {
        try
        {
            FileStream file = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.Open, FileAccess.Read);
            save = _formatter.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("Load");
        }
        catch
        {
            Debug.Log("Save file not found");
            Save();
        }

    }

    public void Save()
    {
        if (save == null)
            save = new SaveData();
        Debug.Log("Save");
        save.LastSaveTime = DateTime.Now;

        FileStream file = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.OpenOrCreate, FileAccess.Write);
        _formatter.Serialize(file, save);
        file.Close();
    }
}
