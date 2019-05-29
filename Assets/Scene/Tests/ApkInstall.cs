using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ApkInstall : MonoBehaviour
{

	[SerializeField] private Button installButton;
	[SerializeField] private Text textDebug;

	private string _path;
	
	// Use this for initialization
	void Start ()
	{
		_path = Path.Combine(Application.persistentDataPath, "MyPAL.apk");
		Debug.Log(_path);
		
		installButton.onClick.AddListener(Install);
		
		#if UNITY_ANDROID
		textDebug.text = getSDKInt().ToString();
		#endif
	}	
	
	static int getSDKInt() {
		using (var version = new AndroidJavaClass("android.os.Build$VERSION")) {
			return version.GetStatic<int>("SDK_INT");
		}
	}

	void Install()
	{
		if (getSDKInt() >= 24)
			InstallApkApi24();
		else
			InstallApk();
	}

	bool InstallApkApi24()
	{
		bool success = true;
		GameObject.Find("TextDebug").GetComponent<Text>().text = "Installing App";
	
		try
		{
			//Get Activity then Context
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject unityContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
	
			//Get the package Name
			string packageName = unityContext.Call<string>("getPackageName");
			string authority = packageName + ".fileprovider";
	
			AndroidJavaClass intentObj = new AndroidJavaClass("android.content.Intent");
			string ACTION_VIEW = intentObj.GetStatic<string>("ACTION_VIEW");
			AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", ACTION_VIEW);
	
	
			int FLAG_ACTIVITY_NEW_TASK = intentObj.GetStatic<int>("FLAG_ACTIVITY_NEW_TASK");
			int FLAG_GRANT_READ_URI_PERMISSION = intentObj.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION");
	
			//File fileObj = new File(String pathname);
			AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", _path);
			//FileProvider object that will be used to call it static function
			AndroidJavaClass fileProvider = new AndroidJavaClass("android.support.v4.content.FileProvider");
			//getUriForFile(Context context, String authority, File file)
			AndroidJavaObject uri = fileProvider.CallStatic<AndroidJavaObject>("getUriForFile", unityContext, authority, fileObj);
	
			intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/vnd.android.package-archive");
			intent.Call<AndroidJavaObject>("addFlags", FLAG_ACTIVITY_NEW_TASK);
			intent.Call<AndroidJavaObject>("addFlags", FLAG_GRANT_READ_URI_PERMISSION);
			currentActivity.Call("startActivity", intent);
	
			GameObject.Find("TextDebug").GetComponent<Text>().text = "Success";
		}
		catch (System.Exception e)
		{
			GameObject.Find("TextDebug").GetComponent<Text>().text = "Error: " + e.Message;
			success = false;
		}
	
		return success;
	}

	bool InstallApk()
	{
		try
		{
			AndroidJavaClass intentObj = new AndroidJavaClass("android.content.Intent");									
			string ACTION_VIEW = intentObj.GetStatic<string>("ACTION_VIEW");
			int FLAG_ACTIVITY_NEW_TASK = intentObj.GetStatic<int>("FLAG_ACTIVITY_NEW_TASK");
			AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", ACTION_VIEW);

			AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", _path);
			AndroidJavaClass uriObj = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uri = uriObj.CallStatic<AndroidJavaObject>("fromFile", fileObj);

			intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/vnd.android.package-archive");
			intent.Call<AndroidJavaObject>("addFlags", FLAG_ACTIVITY_NEW_TASK);
			intent.Call<AndroidJavaObject>("setClassName", "com.android.packageinstaller",
				"com.android.packageinstaller.PackageInstallerActivity");

			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			currentActivity.Call("startActivity", intent);

			GameObject.Find("TextDebug").GetComponent<Text>().text = "Success";
			return true;
		}
		catch (System.Exception e)
		{
			GameObject.Find("TextDebug").GetComponent<Text>().text = "Error: " + e.Message;
			return false;
		}
	}
}
