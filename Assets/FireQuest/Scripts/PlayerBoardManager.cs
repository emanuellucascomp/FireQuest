using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardManager : MonoBehaviour {

	public List<Player> playerList = new List<Player>();

	private void Awake()
	{
		playerList.Clear();
		DatabaseManager.sharedInstance.GetPlayers(result =>
		{
			playerList = result;
			Debug.Log(playerList[0].email);
		});

	}

}
