using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using LitJson;
using UnityEngine;
using _Assetbundle;
using _AssetBundleServer;
using _Version;

public class AssetBundleMessage : INetworkMessage {
	private readonly DownloadDialog _downloadDialog;

	public AssetBundleMessage(DownloadDialog downloadDialog)
	{
		_downloadDialog = downloadDialog;
	}
	
	public void Send(NetworkingPlayer player, Binary frame, NetWorker sender)
	{
		var assetBundleManifest = ByteToObject.ConvertToObject<AssetBundleManifest>(frame.StreamData.CompressBytes());
                
		Debug.Log("Got message");                
		Debug.Log(string.Format("Get url {0}.\n Get version {1}.\n", assetBundleManifest.url, assetBundleManifest.version));
                
		DownloadDialog(assetBundleManifest);
	}
	
	private void DownloadDialog(AssetBundleManifest assetBundleManifest)
	{
		var assetBundleList = JsonMapper.ToObject<AssetBundleList>(assetBundleManifest.assetBundleJson);                              

		if (VersionChecker.IsNewVersionGreater(assetBundleManifest.version))
		{
			MessageBox.ins.ShowOk("Newer version found. Please confirm download.", MessageBox.MsgIcon.msgInformation,
				() => Download(assetBundleManifest, assetBundleList));
		}
	}
	
	private void Download(AssetBundleManifest assetBundleManifest, AssetBundleList assetBundleList)
	{
		if (assetBundleManifest.bookAndActivityJson != null)
		{
			var bookAndActivityList =
				JsonMapper.ToObject<List<BookAndActivityData>>(assetBundleManifest.bookAndActivityJson);
			CreateBookAndActivityData(bookAndActivityList);
		}

		DownloadAssetBundle(assetBundleManifest, assetBundleList);
	}
	
	private void CreateBookAndActivityData(List<BookAndActivityData> bookAndActivityData)
	{
		BookAndActivity.NewBookAndActivity(bookAndActivityData);
	}
	
	private void DownloadAssetBundle(AssetBundleManifest assetBundleManifest, AssetBundleList assetBundleList)
	{
		GameObject.FindObjectOfType<AssetBundleDownloader>().DownloadAssetBundle(assetBundleManifest, assetBundleList, _downloadDialog)
			.onDownloadComplete += () => DownloadComplete(assetBundleManifest.version);
	}
	
	private void DownloadComplete(string newVersion)
	{        
		VersionChecker.SetNewVersion(newVersion);
		MessageBox.ins.ShowOk("MyPAL update complete", MessageBox.MsgIcon.msgInformation, null);
	}
}
