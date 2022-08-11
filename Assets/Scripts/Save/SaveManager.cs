using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get { return _instance; } }
    private static SaveManager _instance;

    public event Action<SaveState> OnLoadEvent;
    public event Action<SaveState> OnSaveEvent;

    public SaveState save;

    private const string SAVE_FILE_NAME = "data.ss";

    private BinaryFormatter _formatter;

    private void Awake()
    {
        _instance = this;
        _formatter = new BinaryFormatter();

        Load();
    }

    public void Load()
    {
        try
        {
            FileStream file = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.Open, FileAccess.Read);
            save = _formatter.Deserialize(file) as SaveState;
            file.Close();
            OnLoadEvent?.Invoke(save);
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
            save = new SaveState();

        save.LastSaveTime = DateTime.Now;

        FileStream file = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.OpenOrCreate, FileAccess.Write);
        _formatter.Serialize(file, save);
        file.Close();

        OnSaveEvent?.Invoke(save);
    }
}
