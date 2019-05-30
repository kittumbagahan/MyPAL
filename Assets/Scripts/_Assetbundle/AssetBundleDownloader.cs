using System;
using System.Collections;
using System.Globalization;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assetbundle
{
	public class AssetBundleDownloader: MonoBehaviour
	{
		public Action onDownloadComplete;
		
		private AssetBundleManifest _assetBundleManifest;
		private AssetBundleList _assetBundleList;
		private DownloadDialog _downloadDialog;
		
		private string FilePath
		{
			get { return Path.Combine(_assetBundleManifest.url, _assetBundleManifest.version); }
		}
		
		public AssetBundleDownloader DownloadAssetBundle([NotNull] AssetBundleManifest assetBundleManifest,
			[NotNull] DownloadDialog downloadDialog)
		{
			Setup(assetBundleManifest, downloadDialog);

			StartCoroutine(DownloadApk_());

			return this;
		}

		private void Setup(AssetBundleManifest assetBundleManifest,
			DownloadDialog downloadDialog)
		{						
			_assetBundleManifest = assetBundleManifest;				
			_downloadDialog = downloadDialog;

			_downloadDialog.gameObject.SetActive(true);
			_downloadDialog.AssetBeingDownloaded.text = "";			
		}

		IEnumerator DownloadApk_()
		{			
			var unityWebRequest = UnityWebRequest(_assetBundleManifest.apkFile);							
			unityWebRequest.SendWebRequest();	
			
			SetAssetBeingDownloaded(_assetBundleManifest.apkFile);
				
			while (!unityWebRequest.isDone)
			{										
				yield return unityWebRequest;

				AssetBundleProgress(unityWebRequest);
				
				if (unityWebRequest.isDone)																											
					break;														
			}					
			

			if(unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
				print(unityWebRequest.error);

			DownloadComplete();
			
			print("All asset bundles downloaded");
		}

		private void SetAssetBeingDownloaded(string fileName)
		{
			_downloadDialog.AssetBeingDownloaded.text = string.Format("Downloading {0}.", fileName);
		}

		private void DownloadComplete()
		{
			_downloadDialog.AssetBeingDownloaded.text = "download complete";
			
			if (onDownloadComplete != null)
				onDownloadComplete();
						
			_downloadDialog.gameObject.SetActive(false	);
		}

		private UnityWebRequest UnityWebRequest(string fileName)
		{
			var file = Path.Combine(FilePath, fileName);
			var unityWebRequest = new UnityWebRequest(file, UnityEngine.Networking.UnityWebRequest.kHttpVerbGET);
			var destinationPath = CheckDirectory() + Path.GetFileName(file);
			var downloadHandlerFile = new DownloadHandlerFile(destinationPath);
			unityWebRequest.downloadHandler = downloadHandlerFile;
			return unityWebRequest;
		}

		private void AssetBundleProgress(UnityWebRequest unityWebRequest)
		{
			_downloadDialog.AssetBundleProgress.fillAmount = unityWebRequest.downloadProgress;
			_downloadDialog.AssetBundleProgressText.text = unityWebRequest.downloadProgress.ToString("P");
		}

		private string CheckDirectory()
		{			
			#if UNITY_EDITOR
				if(!Directory.Exists(Application.dataPath + "/Apk/"))
					Directory.CreateDirectory(Application.dataPath + "/Apk/");
				
				return Application.dataPath + "/Apk/";
#elif UNITY_ANDROID
				if(!Directory.Exists(Application.persistentDataPath + "/Apk/"))
					Directory.CreateDirectory(Application.persistentDataPath + "/Apk/");

				return Application.persistentDataPath + "/Apk/";
#endif
		}
	}
}
