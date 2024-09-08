using Firebase;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private static FirebaseManager instance;
    public static FirebaseManager Instance { get { return instance; } }

	private static FirebaseApp app;
	public static FirebaseApp App { get { return app; } }

	private static FirebaseAuth auth;
	public static FirebaseAuth Auth { get { return auth; } }

	private void Awake()
	{
		CreateInstance();
		CheckFirebaseAvailable();
	}

	private void CheckFirebaseAvailable()
	{
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			var dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available)
			{
				// Create and hold a reference to your FirebaseApp,
				// where app is a Firebase.FirebaseApp property of your application class.
				app = FirebaseApp.DefaultInstance;
				auth = FirebaseAuth.DefaultInstance;

				// Set a flag here to indicate whether Firebase is ready to use by your app.
				Debug.Log("Firebase check success");
			}
			else
			{
				UnityEngine.Debug.LogError(System.String.Format(
				  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
				// Firebase Unity SDK is not safe to use here.
				app = null;
				auth = null;
			}
		});
	}

	private void CreateInstance()
	{ 
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
