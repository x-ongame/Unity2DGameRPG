using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
	public InputField usernameInput;
	public InputField passwordInput;
	public Text maskedPasswordText;
	public Button registerButton;
	public Button goToLoginButton;

	private string actualPassword = "";
	ArrayList credentials;

	// Start is called before the first frame update
	void Start()
	{
		registerButton.onClick.AddListener(writeStuffToFile);
		goToLoginButton.onClick.AddListener(goToLoginScene);
		passwordInput.onValueChanged.AddListener(OnPasswordValueChanged);

		if (File.Exists(Application.dataPath + "/credentials.txt"))
		{
			credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
		}
		else
		{
			File.WriteAllText(Application.dataPath + "/credentials.txt", "");
		}
	}

	void goToLoginScene()
	{
		SceneManager.LoadScene("Home");
	}

	void writeStuffToFile()
	{
		bool isExists = false;

		credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
		foreach (var i in credentials)
		{
			if (i.ToString().Contains(usernameInput.text))
			{
				isExists = true;
				break;
			}
		}

		if (isExists)
		{
			Debug.Log($"Username '{usernameInput.text}' already exists");
		}
		else
		{
			credentials.Add(usernameInput.text + ":" + actualPassword);
			File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));
			Debug.Log("Account Registered");
		}
	}

	void OnPasswordValueChanged(string input)
	{
		actualPassword = passwordInput.text;
		maskedPasswordText.text = new string('*', actualPassword.Length);
		passwordInput.text = actualPassword;
		passwordInput.caretPosition = actualPassword.Length;
	}
}
