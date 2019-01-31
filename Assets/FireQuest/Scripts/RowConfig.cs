using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowConfig : MonoBehaviour {

	public Text score;
	public Text email;
	public Text level;
	public Image profilePic;
	public List<Sprite> imagesList;

	public void Initialize(Player player)
	{
		score.text = player.score.ToString();
		email.text = player.email;
		level.text = player.level.ToString();
		profilePic.sprite = imagesList[Random.Range(0, 2)];


	}
	
}
