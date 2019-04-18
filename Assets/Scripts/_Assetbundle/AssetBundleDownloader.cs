using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using _AssetBundleServer;
using _Version;

namespace _Assetbundle
{
	public class AssetBundleDownloader : MonoBehaviour
	{

		[SerializeField] private Text _appVersion;
		[SerializeField] private Text _appName;

		[SerializeField] private string _url;

		private LauncherNetworking _launcherNetworking;
		private AssetBundleDownloaderView _assetBundleDownloaderView;
		
		// Use this for initialization
		void Start () {
			GetApplicationName();
			GetVersion();

			SetUp();

//			StartCoroutine("DownloadAssetBundle");
		}

		private void SetUp()
		{
			GetComponents();
			
			_assetBundleDownloaderView.ConnectButton.onClick.AddListener(() =>
			{
				_assetBundleDownloaderView.ConnectButton.GetComponentInChildren<Text>().text = "Disconnect";
				_assetBundleDownloaderView.ConnectButton.onClick.AddListener(_launcherNetworking.Quit);
				_launcherNetworking.FindServer();
			});
			
			Subscribe();
		}
		
		private void Subscribe()
		{
			_launcherNetworking.clientDisconnected += ResetConnection;
		}
		
		private void GetComponents()
		{
			_launcherNetworking = GetComponent<LauncherNetworking>();
			_assetBundleDownloaderView = GetComponent<AssetBundleDownloaderView>();
		}

		private void ResetConnection()
		{
			_assetBundleDownloaderView.ConnectButton.GetComponentInChildren<Text>().text = "Connect";
			_assetBundleDownloaderView.UpdateButton.gameObject.SetActive(false);
		}

		private void QuitClient()
		{
			
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
