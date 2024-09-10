using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class UserPanel : MonoBehaviour
{
	private void Start()
	{
		FirebaseManager.DB.
			GetReference("UserData").Child("monglick").
			GetValueAsync().
			ContinueWithOnMainThread(task =>
			{
				if(task.IsCanceled)
				{
					Debug.Log("Get userdata canceled");
					return;
				}
				else if(task.IsFaulted)
				{
					Debug.Log($"Get userdata failed : {task.Exception.Message}");
					return;
				}

				DataSnapshot snapShot = task.Result;
				if(snapShot.Exists)
				{
					string json = snapShot.GetRawJsonValue();
					Debug.Log(json);
					UserData userData = JsonUtility.FromJson<UserData>(json);

					Debug.Log(userData.nickName);
					Debug.Log(userData.level);
					Debug.Log(userData.type);
				}
				else
				{
					UserData userData = new UserData();
				}
			});	
	}
}
