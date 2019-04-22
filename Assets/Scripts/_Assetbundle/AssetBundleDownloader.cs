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
		
		public AssetBundleDownloader DownloadAssetBundle([NotNull] AssetBundleManifest assetBundleManifest, [NotNull] AssetBundleList assetBundleList, [NotNull] DownloadDialog downloadDialog)
		{
			Setup(assetBundleManifest, assetBundleList, downloadDialog);

			StartCoroutine(DownloadAssetBundle_());

			return this;
		}

		private void Setup(AssetBundleManifest assetBundleManifest, AssetBundleList assetBundleList,
			DownloadDialog downloadDialog)
		{						
			_assetBundleManifest = assetBundleManifest;
			_assetBundleList = assetBundleList;
			_downloadDialog = downloadDialog;

			_downloadDialog.gameObject.SetActive(true);
			_downloadDialog.AssetBeingDownloaded.text = "";			
		}

		IEnumerator DownloadAssetBundle_()
		{
			var completed = 0;
			for (int index = 0; index < _assetBundleList.assetBundles.Count; index++)
			{
				var unityWebRequest = UnityWebRequest(index);							
				unityWebRequest.SendWebRequest();	
			
				SetAssetBeingDownloaded(index);
				
				while (!unityWebRequest.isDone)
				{										
					yield return unityWebRequest;

					AssetBundleProgress(unityWebRequest);
					TotalAssetBundleProgress(completed, _assetBundleList.assetBundles.Count);
					if (unityWebRequest.isDone)																											
						break;														
				}					

				//TotalAssetBundleProgress(completed++, _assetBundleList.assetBundles.Count);

				if(unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
					print(unityWebRequest.error);

				completed++;
			}

			DownloadComplete();
			
			print("All asset bundles downloaded");
		}

		private void SetAssetBeingDownloaded(int index)
		{
			_downloadDialog.AssetBeingDownloaded.text = string.Format("Downloading {0}.", _assetBundleList.assetBundles[index]);
		}

		private void DownloadComplete()
		{
			_downloadDialog.AssetBeingDownloaded.text = "download complete";
			
			if (onDownloadComplete != null)
				onDownloadComplete();
						
			_downloadDialog.gameObject.SetActive(false	);
		}

		private UnityWebRequest UnityWebRequest(int index)
		{
			var file = Path.Combine(FilePath, _assetBundleList.assetBundles[index]);
			var unityWebRequest = new UnityWebRequest(file, UnityEngine.Networking.UnityWebRequest.kHttpVerbGET);
			var destinationPath = CheckDirectory() + Path.GetFileName(file);
			var downloadHandlerFile = new DownloadHandlerFile(destinationPath);
			unityWebRequest.downloadHandler = downloadHandlerFile;
			return unityWebRequest;
		}

		private void TotalAssetBundleProgress(int completed, int totalAssets)
		{			
			_downloadDialog.TotalAssetBundleProgressText.text = string.Format("{0}/{1}", completed, totalAssets);
			_downloadDialog.TotalAssetBundleProgress.fillAmount = (((float) completed / totalAssets));
		}

		private void AssetBundleProgress(UnityWebRequest unityWebRequest)
		{
			_downloadDialog.AssetBundleProgress.fillAmount = unityWebRequest.downloadProgress;
			_downloadDialog.AssetBundleProgressText.text = unityWebRequest.downloadProgress.ToString("P");
		}

		private string CheckDirectory()
		{			
			#if UNITY_EDITOR
				if(!Directory.Exists(Application.dataPath + "/AssetBundles/"))
					Directory.CreateDirectory(Application.dataPath + "/AssetBundles/");
				
				return Application.dataPath + "/AssetBundles/";
#elif UNITY_ANDROID
				if(!Directory.Exists(Application.persistentDataPath + "/AssetBundles/"))
					Directory.CreateDirectory(Application.persistentDataPath + "/AssetBundles/");

				return Application.persistentDataPath + "/AssetBundles/";
#endif
		}
	}
}
