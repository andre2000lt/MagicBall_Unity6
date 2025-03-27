using UnityEditor;
using UnityEditor.SceneManagement;

public class ScenesMenu
{

	[MenuItem("Scenes/Pre Scene")]
	static void OpenPreScene()
	{
		OpenScene("Assets/_Game/___Scenes/Pre.unity");
	}


	[MenuItem("Scenes/Ball")]
	static void OpenBallScene()
	{
		OpenScene("Assets/_Game/___Scenes/Ball.unity");
	}


	[MenuItem("Scenes/CreateBall")]
	static void OpenCreateBall()
	{
		OpenScene("Assets/_Game/___Scenes/CreateBall.unity");
	}


	static void OpenScene(string scenePath)
	{
		if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
		{
			EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
		}
	}
}


