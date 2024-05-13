
using Firebase.Auth;
using Google;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthController : MonoBehaviour {
	//Panel
	public GameObject PanelSignIn;
	public GameObject PanelSignUp;
	public GameObject PanelSigned;
	public GameObject PanelProfile;
	public GameObject PanelSettings;
	public GameObject PanelLoading;
	public GameObject PanelPopup;
	public GameObject PanelLog;
	public GameObject PanelPhoneSignIn;

	//Phone
	public Text txtPhoneNumber;
	public Text txtCodeNumber;
	private string phoneId;
	public Text txtBtnGetCode;

	//SignIn
	public Text txtSignInEmail;
	public Text txtSignInPwd;
	public Toggle toggleRememberMe;

	//SignUp
	public Text txtSignUpEmail;
	public Text txtSignUpPwd;
	public Text txtSignUpUsername;

	//Signed
	public Text txtSignedUsername;
	public Image Avata;

	//Popup
	public Text PopupTitle;
	public Text PopupMessage;

	//Log
	public Text txtLog;
	static private int cntLog = 0;
	static private List<string> lstLog = new List<string>();
	private string log;
	private bool isWrite = false;

	//Setting
	public GameObject btnLogout;
	public GameObject grbLogout;

	//Profile
	public Sprite defaultSprite;
	private string imageUrl;
	private string displayName;
	private string email;
	private string id;

	//Google auth variables
	private string webClientId = "639676282029-2obesb1rgv92ft96psvqatl35ba7ic8o.apps.googleusercontent.com";
	private GoogleSignInConfiguration configuration;

	//protected Firebase.Auth.FirebaseAuth auth;
	Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
	Firebase.Auth.FirebaseAuth auth;
	Firebase.Auth.FirebaseUser user;
	PhoneAuthProvider provider;

	private bool isToggleClick = false;
	private	bool isSettingFlag = false;

	void Awake()
	{
		//UI Setting for email sign in
		if (PlayerPrefs.GetInt(Utils.REMEMBER_ME) == 1 && PlayerPrefs.GetInt(Utils.LOGGED) == 1) //Keep auth
		{
			PanelSignIn.SetActive(false);
			PanelSigned.SetActive(true);

			PanelLoading.SetActive(true);
		}

		isSettingFlag = false;

		if (PlayerPrefs.GetInt(Utils.REMEMBER_ME) == 1)
		{
			isToggleClick = true;
			toggleRememberMe.isOn = true;
		}
		else
		{
			isToggleClick = false;
			toggleRememberMe.isOn = false;
		}

		//Setup for Google Sign In
		configuration = new GoogleSignInConfiguration
		{
			WebClientId = webClientId,
			RequestIdToken = true
		};
	}
	
	// Use this for initialization
	void Start () {
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
		{
			dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available)
			{
				WriteLog("Firebase initializing...");
				InitializeFirebase();
			}
			else
			{
				WriteLog("Could not resolve all Firebase dependencies: " + dependencyStatus, "ERR");
			}
		});
	}

	private void Update()
	{
		if(isWrite)
		{
			isWrite = false;

			txtLog.GetComponent<Text>().text = log;
		}
	}

	void InitializeFirebase()
	{
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);

		//Email sign in auto when open app if remember me toggle checked
		if (PlayerPrefs.GetInt(Utils.REMEMBER_ME) == 1 && PlayerPrefs.GetInt(Utils.LOGGED) == 1) //Keep auth
		{
			//SigninAsync(PlayerPrefs.GetString("Email"), PlayerPrefs.GetString("Pwd"));
		}

		//Setup for Facebook Sign In
		/*if (!FB.IsInitialized)
		{
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		}
		else
		{
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}*/
	}

	// Track state changes of the auth object.
	void AuthStateChanged(object sender, System.EventArgs eventArgs)
	{
		WriteLog("Auth State Changed");

		//if (auth.CurrentUser != user)
		//{
		//	bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

		//	user = auth.CurrentUser;
		//	if (signedIn)
		//	{
		//		txtSignedUsername.text = user.DisplayName;
		//	}
		//}
	}

	void OnDestroy()
	{
		auth.StateChanged -= AuthStateChanged;
		auth = null;
	}

	/*#region Email SignIn
	public void SignIn_Click()
	{
		if (string.IsNullOrEmpty(txtSignInEmail.text.Trim()) || string.IsNullOrEmpty(txtSignInPwd.text.Trim()))
		{
			WriteLog("Email or Pwd is null");

			ShowPopup("Error Message", "Email or Password is null");

			return;
		}

		//Show loading panel
		PanelLoading.SetActive(true);

		//Signin
		SigninAsync(txtSignInEmail.text, txtSignInPwd.text);
	}

	public Task SigninAsync(string email, string pwd)
	{
		WriteLog(String.Format("Attempting to sign in as {0}...", email));

		return auth.SignInWithEmailAndPasswordAsync(email, pwd)
		.ContinueWith(HandleEmailSigninResult);
	}

	void HandleEmailSigninResult(Task<Firebase.Auth.FirebaseUser> authTask)
	{
		//Show off loading panel
		PanelLoading.SetActive(false);

		if (authTask.IsCanceled)
		{
			WriteLog("SigninAsync was canceled.");
			ShowPopup("Error Message", "SigninAsync was canceled.\n Please check log for more detail.");
			return;
		}
		if (authTask.IsFaulted)
		{
			WriteLog("SigninAsync encountered an error: " + authTask.Exception);
			ShowPopup("Error Message", "SigninAsync encountered an error.\n Please check log for more detail.");
			return;
		}

		PanelSignIn.SetActive(false);
		PanelSigned.SetActive(true);

		PlayerPrefs.SetInt(Utils.LOGGED, Utils.EM);
		PlayerPrefs.Save();

		WriteLog("Email signin done.");

		if (toggleRememberMe.isOn)
		{
			PlayerPrefs.SetString("Email", txtSignInEmail.text);
			PlayerPrefs.SetString("Pwd", txtSignInPwd.text);
			PlayerPrefs.Save();
		}

		user = auth.CurrentUser;

		if (user != null)
		{
			txtSignedUsername.text = String.Format("Welcome {0}!",user.DisplayName);

			WriteLog("User:" + user.DisplayName);
			WriteLog("PhotoUrl:" + user.PhotoUrl.ToString());

			if (!string.IsNullOrEmpty(user.PhotoUrl.ToString()))
			{
				StartCoroutine(LoadImage(user.PhotoUrl.ToString()));
			}
		}
		else
		{
			WriteLog("User is null.");
		}
	}
	#endregion*/

	/*#region Email SignUp
	public void SignUp_Click()
	{
		if (string.IsNullOrEmpty(txtSignUpEmail.text.Trim()) || string.IsNullOrEmpty(txtSignUpPwd.text.Trim()) || string.IsNullOrEmpty(txtSignUpUsername.text.Trim()))
		{
			WriteLog("Email or Pwd or Username is null");

			ShowPopup("Error Message", "Email or Pwd or Username is null");

			return;
		}

		PanelLoading.SetActive(true);

		CreateUserAsync(txtSignUpEmail.text, txtSignUpPwd.text, txtSignUpUsername.text);
	}

	public void CreateUserAsync(string email, string pwd, string username)
	{
		WriteLog(String.Format("Attempting to create user {0}...", email));

		try
		{
			auth.CreateUserWithEmailAndPasswordAsync(email, pwd).ContinueWith(task =>
			{
				if (task.IsCanceled)
				{
					WriteLog("CreateUserWithEmailAndPasswordAsync was canceled.");
					PanelLoading.SetActive(false);
					return;
				}
				if (task.IsFaulted)
				{
					WriteLog("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
					PanelLoading.SetActive(false);
					return;
				}

				WriteLog("Firebase user created successfully");

				UpdateUserProfileAsync(username);

				PanelLoading.SetActive(false);
				PanelSignIn.SetActive(false);
				PanelSigned.SetActive(true);
			});
		}
		catch (Exception e)
		{
			PanelLoading.SetActive(false);

			WriteLog("Exception:" + e.Message);
		}
		
	}

	void UpdateUserProfileAsync(string username)
	{
		if (auth.CurrentUser == null)
		{
			WriteLog("Not signed in, unable to update user profile");
			return;
		}

		WriteLog("Updating user profile");

		user = auth.CurrentUser;
		if (user != null)
		{
			Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
			{
				DisplayName = username,
				PhotoUrl = user.PhotoUrl,
			};

			user.UpdateUserProfileAsync(profile).ContinueWith(task => {
				if (task.IsCanceled)
				{
					WriteLog("UpdateUserProfileAsync was canceled.");
					return;
				}
				if (task.IsFaulted)
				{
					WriteLog("UpdateUserProfileAsync encountered an error: " + task.Exception);
					return;
				}
				if (task.IsCompleted)
				{
					WriteLog("User profile updated completed");
				}


				PlayerPrefs.SetInt(Utils.LOGGED, Utils.EM);
				PlayerPrefs.Save();

				user = auth.CurrentUser;
				txtSignedUsername.text = String.Format("Welcome {0}!", user.DisplayName);

				//WriteLog("PhotoUrl:" + user.PhotoUrl.ToString());
				//if (!string.IsNullOrEmpty(user.PhotoUrl.ToString()))
				//{
				//	StartCoroutine(LoadImage(user.PhotoUrl.ToString()));
				//}
			});
		}
	}
	#endregion*/

	/*#region UI Event
	public void GotoSignUp()
	{
		PanelSignIn.SetActive(false);
		PanelSignUp.SetActive(true);
	}

	//Goto SignIn panel from SignUp or after logout from Panel Signed
	public void GotoSignIn()
	{
		PanelSignIn.SetActive(true);
		PanelSignUp.SetActive(false);
		PanelSigned.SetActive(false);
		PanelPhoneSignIn.SetActive(false);
	}

	public void ToggleRememberMe_Click()
	{
		if (!isToggleClick)
		{
			isToggleClick = true;
			return;
		}

		if (toggleRememberMe.isOn)
		{
			PlayerPrefs.SetInt(Utils.REMEMBER_ME, 1);
		}
		else
		{
			PlayerPrefs.SetInt(Utils.REMEMBER_ME, 0);
		}

		PlayerPrefs.Save();
	}

	public void Setting_Click()
	{
		if(PlayerPrefs.GetInt(Utils.LOGGED) != 0)
		{
			btnLogout.SetActive(true);
			grbLogout.SetActive(true);
		}
		else
		{
			btnLogout.SetActive(false);
			grbLogout.SetActive(false);
		}

		if (!isSettingFlag)
		{
			isSettingFlag = true;
			PanelProfile.SetActive(false);
			PanelSettings.SetActive(true);
		}
		else
		{
			isSettingFlag = false;
			PanelProfile.SetActive(true);
			PanelSettings.SetActive(false);
		}
	}
	#endregion*/

	#region Firebase Logout
	/// <summary>
	/// Logout Firebase + logout thirt party
	/// </summary>
	public void Logout_Click()
	{
		auth.SignOut();

		if (PlayerPrefs.GetInt(Utils.LOGGED) == Utils.FB)
		{
		//	FB.LogOut();
		}
		else if (PlayerPrefs.GetInt(Utils.LOGGED) == Utils.GG)
		{
			GoogleSignIn.DefaultInstance.SignOut();
		}
		else if (PlayerPrefs.GetInt(Utils.LOGGED) == Utils.TW)
		{
			//Twitter.LogOut();
		}


		PlayerPrefs.SetInt(Utils.LOGGED, Utils.NONE);
		PlayerPrefs.Save();

		PanelProfile.SetActive(true);
		PanelSettings.SetActive(false);
		PanelPhoneSignIn.SetActive(false);
		PanelSigned.SetActive(false);
		PanelSignUp.SetActive(false);
		PanelSignIn.SetActive(true);

		txtSignedUsername.text = "User Name";
		Avata.sprite = defaultSprite;
	}
	#endregion

	#region Google SignIn
	/// <summary>
	/// Google Sign-In Click
	/// </summary>
	public void GoogleSignIn_Click()
	{
		//Sign-In with Google as first to get token for Firebase Auth
		OnGoogleSignIn();
	}

	void OnGoogleSignIn()
	{
		GoogleSignIn.Configuration = configuration;
		GoogleSignIn.Configuration.UseGameSignIn = false;
		GoogleSignIn.Configuration.RequestIdToken = true;

		GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
		  OnGoogleAuthenticationFinished);
	}

	//Handle when Google Sign In successfully
	void OnGoogleAuthenticationFinished(Task<GoogleSignInUser> task)
	{
		if (task.IsFaulted)
		{
			using (IEnumerator<System.Exception> enumerator =
					task.Exception.InnerExceptions.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					GoogleSignIn.SignInException error =
							(GoogleSignIn.SignInException)enumerator.Current;
					WriteLog("Got Error: " + error.Status + " " + error.Message, "ERR");
				}
				else
				{
					WriteLog("Got Unexpected Exception?!?" + task.Exception, "ERR");
				}
			}
		}
		else if (task.IsCanceled)
		{
			WriteLog("Canceled");
		}
		else
		{
			WriteLog("Google Sign-In successed");

			WriteLog("IdToken: " +task.Result.IdToken);
		

			//Start Firebase Auth
			Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
			auth.SignInWithCredentialAsync(credential).ContinueWith(t =>
			{
				if (t.IsCanceled)
				{
					WriteLog("SignInWithCredentialAsync was canceled.");
					return;
				}
				if (t.IsFaulted)
				{
					WriteLog("SignInWithCredentialAsync encountered an error: " + t.Exception);
					return;
				}

				PlayerPrefs.SetInt(Utils.LOGGED, Utils.GG);
				PlayerPrefs.Save();

				user = auth.CurrentUser;
				txtSignedUsername.text = String.Format("Welcome {0}!", user.DisplayName);

				PanelSignIn.SetActive(false);
				PanelSignUp.SetActive(false);
				PanelSigned.SetActive(true);

				//WriteLog("PhotoUrl:" + user.PhotoUrl.AbsoluteUrlOrEmptyString());

			//	StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.AbsoluteUrlOrEmptyString())));
			});
		}
	}
	#endregion



	#region Popup
	public void OK_Click()
	{
		PanelPopup.SetActive(false);
	}

	public void Log_Click()
	{
		PanelPopup.SetActive(false);
		PanelLog.SetActive(true);
	}

	private void ShowPopup(string title,string mes)
	{
		PopupTitle.text = title;
		PopupMessage.text = mes;

		PanelPopup.SetActive(true);
	}
	#endregion

	#region Log
	public void CloseLog_Click()
	{
		PanelLog.SetActive(false);
	}

	public void WriteLog(string mes, string logType = "INF")
	{
		StackFrame frame = new StackFrame(1);
		cntLog++;

		lstLog.Add(cntLog.ToString() + ". [" + frame.GetMethod().Name + "] " + mes + "\n");

		string txt = "";
		foreach (string s in lstLog)
		{
			txt += s;
		}

		log = txt;
		isWrite = true;
	}
	#endregion

	/// <summary>
	/// Return imageUrl from Firebase, if it null, return imageUrl from thirt party(google, facebook,...)
	/// </summary>
	/// <param name="url">imageUrl from Firebase</param>
	/// <returns>imageUrl</returns>

}
