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

    private const string SAVE_FILE_NAME = "data.ss";

    private SaveState _save;
    private BinaryFormatter _formatter;

    private void Awake()
    {
        _formatter = new BinaryFormatter();

        Load();
    }

    private void Load()
    {
        try
        {
            FileStream file = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.Open, FileAccess.Read);
            _save = _formatter.Deserialize(file) as SaveState;
            file.Close();
            OnLoadEvent?.Invoke(_save);
        }
        catch
        {
            Debug.Log("Save file not found");
            Save();
        }

    }

    private void Save()
    {
        if (_save == null)
            _save = new SaveState();

        _save.LastSaveTime = DateTime.Now;

        FileStream file = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.OpenOrCreate, FileAccess.Write);
        _formatter.Serialize(file, _save);
        file.Close();

        OnSaveEvent?.Invoke(_save);
    }
}
