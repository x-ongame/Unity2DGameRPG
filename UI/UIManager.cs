using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
	[SerializeField] private Button changeSceneButton;
	[SerializeField] private string sceneNameToLoad;

	private void Awake()
	{
		if (changeSceneButton != null)
		{
			changeSceneButton.onClick.AddListener(ChangeScene);
		}
	}

	private void ChangeScene()
	{
		SceneManager.LoadScene(sceneNameToLoad);
	}
}
