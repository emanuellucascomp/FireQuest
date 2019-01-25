using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DatabaseManager : MonoBehaviour {

	public static DatabaseManager sharedInstance = null;

	/// <summary>
	/// Awake this instance and initialize sharedInstance for Singleton pattern
	/// </summary>
	void Awake() {
		if (sharedInstance == null) {
			sharedInstance = this;
		} else if (sharedInstance != this) {
			Destroy (gameObject);  
		}

		DontDestroyOnLoad(gameObject);
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://firequest-d3b6d.firebaseio.com/");
		Debug.Log(Router.Players());

	}

	public void CreateNewPlayer(Player player, string uid)
	{
		string json = JsonUtility.ToJson(player);
		Router.PlayerWithUid(uid).SetRawJsonValueAsync(json);
	}

	public void GetPlayers(Action<List<Player>> completionBlock)
	{
		List<Player> tempList = new List<Player>();
		Router.Players().GetValueAsync().ContinueWith(task =>
		{
			DataSnapshot player = task.Result;
			foreach(DataSnapshot playerNode in player.Children)
			{
				var playerDict = (IDictionary<string, object>)playerNode.Value;
				Player newPlayer = new Player(playerDict);
				tempList.Add(newPlayer);
			}

			completionBlock(tempList);
		});
	}

}
