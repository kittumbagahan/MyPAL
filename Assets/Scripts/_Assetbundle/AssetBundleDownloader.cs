using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using _Version;

namespace _Assetbundle
{
	public class AssetBundleDownloader : MonoBehaviour
	{

		[SerializeField] private Text _appVersion;
		[SerializeField] private Text _appName;

		[SerializeField] private string _url;
		
		// Use this for initialization
		void Start () {
			GetApplicationName();
			GetVersion();						

//			StartCoroutine("DownloadAssetBundle");
		}
	
		// Update is called once per frame
		void Update () {
		
		}	

		private void GetVersion()
		{//
			_appVersion.text = VersionChecker.Version();
		}

		private void GetApplicationName()
		{
			_appName.text = Application.productName;
		}


		IEnumerator DownloadAssetBundle()
		{
			var file = Path.Combine(_url, "bookshelf");			
			var unityWebRequest = new UnityWebRequest(file, UnityWebRequest.kHttpVerbGET);
			var destinationPath = Application.dataPath + "/AssetBundles/" + Path.GetFileName(file);
			
			unityWebRequest.downloadHandler =
				new DownloadHandlerFile(destinationPath);
			
			yield return unityWebRequest.SendWebRequest();
			
			if(unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
				Debug.LogError(unityWebRequest.error);
			else
				Debug.Log("File successfully downloaded and saved to " + destinationPath);
		}
	}
}
