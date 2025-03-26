using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorage<T> where T : new()
{
    void Save(T data);
    T Load();
    bool IsStorageEmpty(string key);
}
