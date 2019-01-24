using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;

public class AuthManager : MonoBehaviour {

	Firebase.Auth.FirebaseAuth auth;
	public delegate IEnumerator AuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation);
	public event AuthCallback authCallback;

	private void Awake()
	{
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
	}

	public void SignUpNewUser(string email, string password)
	{
		auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			StartCoroutine(authCallback(task, "sign_up"));
		});
	}

	public void LogInExistingUser(string email, string password)
	{
		auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			StartCoroutine(authCallback(task, "login"));
		});
	}
}
