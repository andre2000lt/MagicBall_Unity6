using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntryPoint : MonoBehaviour
{
    [SerializeField] private StorageProviderSO _storageProvider;
    private void Awake()
    {
        BallsStorage.Init(_storageProvider);


    }
}
