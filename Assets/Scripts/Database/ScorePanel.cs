using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
	private void OnEnable()
	{
		FirebaseManager.DB
			.GetReference("ScoreBoard")
			.OrderByChild("score")
			.LimitToLast(3)
			.ValueChanged += ScoreBoardChanged;
	}

	private void OnDisable()
	{
		FirebaseManager.DB
			.GetReference("ScoreBoard")
			.OrderByChild("score")
			.LimitToLast(3)
			.ValueChanged -= ScoreBoardChanged;
	}

	private void Start()
	{

		/*.GetValueAsync()
		.ContinueWithOnMainThread(task =>
		{
			if(task.IsCanceled)
			{
				return;
			}
			else if(task.IsFaulted)
			{
				return;
			}

			DataSnapshot snapshot = task.Result;
			foreach(DataSnapshot child in snapshot.Children)
			{
				string json = child.GetRawJsonValue();
				ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);

				Debug.Log($"{scoreData.nickName} - {scoreData.score}");
			}
		});*/
	}

	private void ScoreBoardChanged(object sendor, ValueChangedEventArgs args)
	{
		DataSnapshot snapshot = args.Snapshot;
		List<DataSnapshot> sorted = new List<DataSnapshot>(snapshot.Children);
		sorted.Reverse();

		foreach (DataSnapshot child in sorted)
		{
			string json = child.GetRawJsonValue();
			ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);

			Debug.Log($"{scoreData.nickName} - {scoreData.score}");
		}
	}
}

[SerializeField]

public class ScoreData
{
	public string nickName;
	public int score;
}
