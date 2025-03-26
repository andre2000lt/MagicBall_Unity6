using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerPrefsStorage<T> : IStorage<T> where T : new()    
{
    private const string DEFAULT_KEY = "PlayerPrefsStorage";
    private string _key;

    public PlayerPrefsStorage(string key)
    {
        _key = (key != null)? key : DEFAULT_KEY;
    }


    public void Save(T data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(_key, jsonData);
    }


    public T Load()
    {
        var data = new T();

        if(PlayerPrefs.HasKey(_key))
        {
            string jsonData = PlayerPrefs.GetString(_key);
            data = JsonConvert.DeserializeObject<T>(jsonData);
        }

        return data;
    }


    public bool IsStorageEmpty(string key)
    {
        return !PlayerPrefs.HasKey(_key);
    }
}
