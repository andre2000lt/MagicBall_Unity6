using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class HttpRequest
{
    private const string SERVER_URL = "https://pcgame.lt/magic/magic.php";

    public static async void UploadFormToServer(Dictionary<string, string> formData, string url = SERVER_URL)
    {
        WWWForm form = new WWWForm();
        foreach (var item in formData)
        {
            form.AddField(item.Key, item.Value);
        }

        UnityWebRequest request = UnityWebRequest.Post(url, form);

        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

        Debug.Log(request.downloadHandler.text);

        request.Dispose();
    }
}
