using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class TextFileStorage<T> : IStorage<T> where T : new()
{
    private const string DEFAULT_KEY = "TextFileStorage";
    private string _key;
    private string _path;
    
    
    public TextFileStorage(string key = null)
    {
        _key = (key != null) ? key : DEFAULT_KEY;
        string filename = _key + ".txt";
        _path = Path.Combine(Application.persistentDataPath, filename);
        Debug.Log(_path);
    }


    public T Load() 
    {
        var data = new T(); 

        if(File.Exists(_path))
        {
            string jsonData = File.ReadAllText(_path);
            data = JsonConvert.DeserializeObject<T>(jsonData);
        }

        return data;    
    }

    public void Save(T data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(_path, jsonData);
    }


    public bool IsStorageEmpty(string key)
    {
        return !File.Exists(_path);
    }
}
