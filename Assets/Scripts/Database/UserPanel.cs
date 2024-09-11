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
					Debug.Log("취소");
				}
				else if(task.IsFaulted)
				{
					Debug.Log("오류");
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
					Debug.Log("취소");
				}
				else if (task.IsFaulted)
				{
					Debug.Log("오류");
				}

				nickNameText.text = nickName;
				Debug.Log("닉네임 재설정 완료");
			});
	}
}
