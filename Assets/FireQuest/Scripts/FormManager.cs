using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;

public class FormManager : MonoBehaviour {

	// UI objects linked from the inspector
	public InputField emailInput;
	public InputField passwordInput;

	public Button signUpButton;
	public Button loginButton;

	public Text statusText;

	public AuthManager authManager;

	void Awake() {
		ToggleButtonStates (false);
		authManager.authCallback += HandleAuthCallback;
	}

	/// <summary>
	/// Validates the email input
	/// </summary>
	public void ValidateEmail() {
		string email = emailInput.text;
		var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

		if (email != "" && Regex.IsMatch(email, regexPattern)) {
			ToggleButtonStates (true);
		} else {
			ToggleButtonStates (false);
		}
	}

	// Firebase methods
	public void OnSignUp() {
		authManager.SignUpNewUser(emailInput.text, passwordInput.text);
		Debug.Log ("Sign Up");
	}

	public void OnLogin() {
		authManager.LogInExistingUser(emailInput.text, passwordInput.text);
		Debug.Log ("Login");
	}

	IEnumerator HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation)
	{
		if (task.IsCanceled)
		{
			UpdateStatus(operation + " was canceled.");
		}
		if (task.IsFaulted)
		{
			UpdateStatus(operation + " encountered an error: " + task.Exception);
		} else if (task.IsCompleted)
		{
			if(operation == "sign_up"){
				Firebase.Auth.FirebaseUser newUser = task.Result;
				Debug.Log("Welcome");
				Player player = new Player(newUser.Email, 0, 1);
				DatabaseManager.sharedInstance.CreateNewPlayer(player, newUser.UserId);

			}
			// Firebase user has been created.
			UpdateStatus("Loading game scene...");
			yield return new WaitForSeconds(1.5f);
			SceneManager.LoadScene("Player List");

		}
	}

	private void OnDestroy()
	{
		authManager.authCallback -= HandleAuthCallback;
	}

	// Utilities
	private void ToggleButtonStates(bool toState) {
		signUpButton.interactable = toState;
		loginButton.interactable = toState;
	}

	private void UpdateStatus(string message) {
		statusText.text = message;
	}
}
