using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ApkInstall
{	

	private readonly string _path;	
	// Use this for initialization	

	public ApkInstall(string path)
	{
		_path = path;
	}
	
	private int getSDKInt() {
		using (var version = new AndroidJavaClass("android.os.Build$VERSION")) {
			return version.GetStatic<int>("SDK_INT");
		}
	}

	public void Install()
	{
		if (getSDKInt() >= 24)
			InstallApkApi24();
		else
			InstallApk();
	}

	bool InstallApkApi24()
	{		
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
				
			AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", _path);			
			AndroidJavaClass fileProvider = new AndroidJavaClass("android.support.v4.content.FileProvider");			
			AndroidJavaObject uri = fileProvider.CallStatic<AndroidJavaObject>("getUriForFile", unityContext, authority, fileObj);
	
			intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/vnd.android.package-archive");
			intent.Call<AndroidJavaObject>("addFlags", FLAG_ACTIVITY_NEW_TASK);
			intent.Call<AndroidJavaObject>("addFlags", FLAG_GRANT_READ_URI_PERMISSION);
			currentActivity.Call("startActivity", intent);				
		}
		catch (System.Exception e)
		{
			ErrorMessage();
			return false;
		}
	
		return true;
	}

	bool InstallApk()
	{
		try
		{
			AndroidJavaClass intentObj = new AndroidJavaClass("android.content.Intent");									
			string ACTION_VIEW = intentObj.GetStatic<string>("ACTION_VIEW");
			int FLAG_ACTIVITY_NEW_TASK = intentObj.GetStatic<int>("FLAG_ACTIVITY_CLEAR_TOP");
			AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", ACTION_VIEW);

			AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", _path);
			AndroidJavaClass uriObj = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uri = uriObj.CallStatic<AndroidJavaObject>("fromFile", fileObj);

			intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/vnd.android.package-archive");
			intent.Call<AndroidJavaObject>("setFlags", FLAG_ACTIVITY_NEW_TASK);

			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			currentActivity.Call("startActivity", intent);
			
			return true;
		}
		catch (System.Exception e)
		{
			ErrorMessage();		
			return false;
		}
	}

	private void ErrorMessage()
	{
		MessageBox.ins.ShowOk(string.Format("Error: {0}", e.Message), MessageBox.MsgIcon.msgError, null);
	}
}
