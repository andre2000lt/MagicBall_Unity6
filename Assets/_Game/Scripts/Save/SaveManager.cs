using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveManager
{
    public static void Save<T>(string key, T saveData) {
        string jsonData = JsonConvert.SerializeObject(saveData);
        PlayerPrefs.SetString(key, jsonData);
    }

    public static T Get<T>(string key) where T: new() {
        if (PlayerPrefs.HasKey(key)) {
            string jsonData = PlayerPrefs.GetString(key);
            return JsonConvert.DeserializeObject<T>(jsonData);
        } else {   
            return default(T);   
        }
    }
}
