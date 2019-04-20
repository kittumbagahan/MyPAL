using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace _Assetbundle
{
	public class AssetBundleDownloader: MonoBehaviour
	{
		public Action onDownloadComplete;
		
		private AssetBundleManifest _assetBundleManifest;
		private AssetBundleList _assetBundleList;
		private Image _progressBar;
		
		private string FilePath
		{
			get { return Path.Combine(_assetBundleManifest.url, _assetBundleManifest.version); }
		}
		
		public AssetBundleDownloader DownloadAssetBundle(AssetBundleManifest assetBundleManifest, AssetBundleList assetBundleList, Image progressBar)
		{
			_assetBundleManifest = assetBundleManifest;
			_assetBundleList = assetBundleList;
			_progressBar = progressBar;
			
			StartCoroutine(DownloadAssetBundle_());

			return this;
		}
	
		IEnumerator DownloadAssetBundle_()
		{
			var completed = 0;
			for (int index = 0; index < _assetBundleList.assetBundles.Count; index++)
			{
				var file = Path.Combine(FilePath, _assetBundleList.assetBundles[index]);
				var unityWebRequest = new UnityWebRequest(file, UnityWebRequest.kHttpVerbGET);
				var destinationPath = CheckDirectory() + Path.GetFileName(file);
				var downloadHandlerFile = new DownloadHandlerFile(destinationPath);
				unityWebRequest.downloadHandler = downloadHandlerFile;					
			
				unityWebRequest.SendWebRequest();	
			
				while (!unityWebRequest.isDone)
				{										
					yield return unityWebRequest;

					if (unityWebRequest.isDone)
					{										
						print("File successfully downloaded and saved to " + destinationPath);	
						_progressBar.fillAmount = unityWebRequest.downloadProgress;
						break;						
					}

					print("file " + destinationPath + ", progress: " + unityWebRequest.downloadProgress);
					_progressBar.fillAmount = unityWebRequest.downloadProgress;
				}							
												
				if(unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
					print(unityWebRequest.error);
				
			}

			if (onDownloadComplete != null)
				onDownloadComplete();
			
			print("All asset bundles downloaded");
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
