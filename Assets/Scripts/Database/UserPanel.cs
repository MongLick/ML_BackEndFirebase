using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
	[SerializeField] TMP_Text nickNameText;
	[SerializeField] TMP_InputField nickNameInputField;
	[SerializeField] Button nickNameChangeButton;

	private UserData userData;

	private void Awake()
	{
		nickNameChangeButton.onClick.AddListener(ChangeNickName);
	}

	private void Start()
	{
		FirebaseManager.DB
			.GetReference("UserData")
			.Child(FirebaseManager.Auth.CurrentUser.UserId)
			.GetValueAsync()
			.ContinueWithOnMainThread(task =>
			{
				if(task.IsCanceled)
				{
					Debug.Log("���");
				}
				else if(task.IsFaulted)
				{
					Debug.Log("����");
				}

				DataSnapshot snapShot = task.Result;
				if(snapShot.Exists)
				{
					string json = snapShot.GetRawJsonValue();
					Debug.Log(json);

					userData = JsonUtility.FromJson<UserData>(json);
					nickNameText.text = userData.nickName;
				}
				else
				{
					userData = new UserData();
					nickNameText.text = userData.nickName;
				}
			});
	}

	public void ChangeNickName()
	{
		string nickName = nickNameInputField.text;
		FirebaseManager.DB
			.GetReference("UserData")
			.Child(FirebaseManager.Auth.CurrentUser.UserId)
			.Child("nickName")
			.SetValueAsync(nickName)
			.ContinueWithOnMainThread(task =>
			{
				if (task.IsCanceled)
				{
					Debug.Log("���");
				}
				else if (task.IsFaulted)
				{
					Debug.Log("����");
				}

				nickNameText.text = nickName;
				Debug.Log("�г��� �缳�� �Ϸ�");
			});
	}
}
