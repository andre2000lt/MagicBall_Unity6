
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameScenesController
{
    public static async void LoadSceneAsync(GameScene scene)
    {
        LoadingScreen.Instance.Show();
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(scene.ToString());
        loadAsync.allowSceneActivation = false;
        while (loadAsync.progress < 0.9f) 
        {
            LoadingScreen.Instance.SetProgressBarValue(loadAsync.progress);
            await Task.Yield();
        }
        loadAsync.allowSceneActivation = true;
        LoadingScreen.Instance.Hide();
    }
}
