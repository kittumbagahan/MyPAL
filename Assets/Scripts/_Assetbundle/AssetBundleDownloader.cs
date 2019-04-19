using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assetbundle
{
	public class AssetBundleDownloader: MonoBehaviour
	{
		private string _url;
		private string _version;
		private List<string> _assetBundles;		

		private string FilePath
		{
			get { return Path.Combine(_url, _version); }
		}
		
		public void DownloadAssetBundle(string url, string version, List<string> assetBundles)
		{
			_url = url;
			_version = version;
			_assetBundles = assetBundles;
			StartCoroutine(DownloadAssetBundle_());
		}
	
		IEnumerator DownloadAssetBundle_()
		{										
			var file = Path.Combine(FilePath, _assetBundles[0]);
			var unityWebRequest = new UnityWebRequest(file, UnityWebRequest.kHttpVerbGET);
			var destinationPath = Application.dataPath + "/AssetBundles/" + Path.GetFileName(file);
			var downloadHandlerFile = new DownloadHandlerFile(destinationPath);
			unityWebRequest.downloadHandler = downloadHandlerFile;					
			
			yield return unityWebRequest.SendWebRequest();	
			
//			while (!unityWebRequest.isDone)
//			{
//				Debug.Log("file " + destinationPath + ", progress: " + unityWebRequest.downloadProgress);
//				yield return unityWebRequest;
//			}							
				
			if(unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
				print(unityWebRequest.error);
			else				
				print("File successfully downloaded and saved to " + destinationPath);
		}
	}
}
