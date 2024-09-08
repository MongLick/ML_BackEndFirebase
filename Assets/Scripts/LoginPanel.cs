using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
	[SerializeField] TMP_InputField idInputField;
	[SerializeField] TMP_InputField passInputField;

	[SerializeField] Button loginBuuon;
	[SerializeField] Button signUpButton;

	private void Awake()
	{
		loginBuuon.onClick.AddListener(Login);
		signUpButton.onClick.AddListener(SignUp);
	}

	private void Login()
	{
		string email = idInputField.text;
		string password = passInputField.text; 

		FirebaseManager.Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
		{
			if (task.IsCanceled)
			{
				Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted)
			{
				Debug.LogError(task.Exception.Message);
				return;
			}

			Firebase.Auth.AuthResult result = task.Result;
			Debug.LogFormat("User signed in successfully: {0} ({1})",
				result.User.DisplayName, result.User.UserId);
		});
	}

	private void SignUp()
	{
		string email = idInputField.text;
		string password = passInputField.text;

		FirebaseManager.Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			if (task.IsCanceled)
			{
				Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted)
			{
				Debug.LogError(task.Exception.Message);
				return;
			}

			// Firebase user has been created.
			Firebase.Auth.AuthResult result = task.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})",
				result.User.DisplayName, result.User.UserId);
		});
	}
}
