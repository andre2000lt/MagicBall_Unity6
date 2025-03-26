using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Storage Provider")]
public class StorageProviderSO : ScriptableObject
{
    [SerializeField] private StorageType _storageType;


    public IStorage<T> GetStorage<T>(string key = null) where T : new()
    {
        switch (_storageType)
        {
            case StorageType.PlayerPrefs:
                return new PlayerPrefsStorage<T>(key);            
            case StorageType.TextFile:
                return new TextFileStorage<T>(key);
            default: return null;
        }
    }


    enum StorageType
    {
        PlayerPrefs,
        TextFile
    }

}
