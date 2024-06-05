using UnityEngine;
using UnityEngine.UI;

public class PasswordMask : MonoBehaviour
{
	public InputField passwordInputField;
	public Text maskedText;

	private string actualPassword = "";

	void Start()
	{
		passwordInputField.onValueChanged.AddListener(OnPasswordValueChanged);
		maskedText.text = "";
	}

	void OnPasswordValueChanged(string input)
	{
		actualPassword = passwordInputField.text;
		maskedText.text = new string('*', actualPassword.Length);
		passwordInputField.text = actualPassword;
		passwordInputField.caretPosition = actualPassword.Length;
	}
}
