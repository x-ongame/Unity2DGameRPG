using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
	public InputField usernameInput;
	public InputField passwordInput;
	public Text maskedPasswordText;
	public Button loginButton;
	public Button goToRegisterButton;

	private string actualPassword = "";
	ArrayList credentials;

	// Start is called before the first frame update
	void Start()
	{
		loginButton.onClick.AddListener(login);
		goToRegisterButton.onClick.AddListener(moveToRegister);
		passwordInput.onValueChanged.AddListener(OnPasswordValueChanged);

		if (File.Exists(Application.dataPath + "/credentials.txt"))
		{
			credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
		}
		else
		{
			Debug.Log("Credential file doesn't exist");
		}
	}

	void OnPasswordValueChanged(string input)
	{
		actualPassword = passwordInput.text;
		maskedPasswordText.text = new string('*', actualPassword.Length);
		passwordInput.text = actualPassword;
		passwordInput.caretPosition = actualPassword.Length;
	}

	void login()
	{
		bool isExists = false;

		credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

		foreach (var i in credentials)
		{
			string line = i.ToString();
			if (line.Substring(0, line.IndexOf(":")).Equals(usernameInput.text) &&
				line.Substring(line.IndexOf(":") + 1).Equals(actualPassword))
			{
				isExists = true;
				break;
			}
		}

		if (isExists)
		{
			Debug.Log($"Logging in '{usernameInput.text}'");
			loadWelcomeScreen();
		}
		else
		{
			Debug.Log("Incorrect credentials");
		}
	}

	void moveToRegister()
	{
		SceneManager.LoadScene("Register");
	}

	void loadWelcomeScreen()
	{
		SceneManager.LoadScene("Village");
	}
}
