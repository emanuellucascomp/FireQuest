using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class ProfileConfig : MonoBehaviour {

	public Text emailString;
	public Image profilePic;

	public void Config(Firebase.Auth.FirebaseUser user)
	{
		emailString.text = string.Format(user.Email);

	}
	
}
