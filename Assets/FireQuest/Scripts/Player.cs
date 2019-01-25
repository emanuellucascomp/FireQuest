using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player {

	public string email;
	public int score;
	public int level;

	public Player(string email, int score, int level)
	{
		this.email = email;
		this.score = score;
		this.level = level;
	}

	public Player(IDictionary<string, object> dict)
	{
		this.email = dict["email"].ToString();
		this.score = Convert.ToInt32(dict["score"]);
		this.level = Convert.ToInt32(dict["level"]);

	}

}
