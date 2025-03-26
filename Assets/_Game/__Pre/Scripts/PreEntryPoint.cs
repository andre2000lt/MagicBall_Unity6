using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class PreEntryPoint : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadAllServices());
    }


    private IEnumerator LoadAllServices()
    {

        AdmobListener.Init();
        //AppodealListener.Init();

        GamePlayStatictic.Init();



        GamePlayEventBus.InvokeGameStartedEvent();

        yield return StartCoroutine(LoadLocales());

        AsyncOperation loadSceneAsynch = SceneManager.LoadSceneAsync("Ball");
        loadSceneAsynch.allowSceneActivation = false;
        yield return StartCoroutine(LoadSceneAsync(loadSceneAsynch));
        loadSceneAsynch.allowSceneActivation = true;


    }


    private IEnumerator LoadLocales()
    {
        while(!LocalizationSettings.InitializationOperation.IsDone)
        {
            yield return null;
        }

        SettingsUI.SetSavedlocale();
    }


    private IEnumerator LoadSceneAsync(AsyncOperation loadSceneAsynch)
    {
        while (loadSceneAsynch.progress < 0.9f)
        {
            yield return null;
        }
    }
}
