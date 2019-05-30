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

	private AssetBundleManifest _assetBundleManifest;
	
	public AssetBundleMessage(DownloadDialog downloadDialog)
	{
		_downloadDialog = downloadDialog;
	}
	
	public void Send(NetworkingPlayer player, Binary frame, NetWorker sender)
	{
		var assetBundleManifest = ByteToObject.ConvertTo<AssetBundleManifest>(frame.StreamData.CompressBytes());
                
		Debug.Log("Got message");                
		Debug.Log(string.Format("Get url {0}.\n Get version {1}.\n", assetBundleManifest.url, assetBundleManifest.version));
                
		DownloadDialog(assetBundleManifest);
	}
	
	private void DownloadDialog(AssetBundleManifest assetBundleManifest)
	{
		_assetBundleManifest = assetBundleManifest;
		
		if (VersionChecker.IsNewVersionGreater(_assetBundleManifest.version))
		{
			MessageBox.ins.ShowOk("Newer version found. Please confirm download.", MessageBox.MsgIcon.msgInformation,
				() => Download());
		}
	}
	
	private void Download()
	{
		if (_assetBundleManifest.bookAndActivityJson != null)
		{
			var bookAndActivityList =
				JsonMapper.ToObject<List<BookAndActivityData>>(_assetBundleManifest.bookAndActivityJson);
			CreateBookAndActivityData(bookAndActivityList);
		}

		DownloadAssetBundle();
	}
	
	private void CreateBookAndActivityData(List<BookAndActivityData> bookAndActivityData)
	{
		BookAndActivity.NewBookAndActivity(bookAndActivityData);
	}
	
	private void DownloadAssetBundle()
	{
		GameObject.FindObjectOfType<AssetBundleDownloader>().DownloadAssetBundle(_assetBundleManifest, _downloadDialog)
			.onDownloadComplete += () => DownloadComplete(_assetBundleManifest.version);
	}
	
	private void DownloadComplete(string newVersion)
	{        
		VersionChecker.SetNewVersion(newVersion);
		MessageBox.ins.ShowOk("MyPAL update complete.\nInstallation will now proceed.", MessageBox.MsgIcon.msgInformation, InstallApk);
	}

	private void InstallApk()
	{
		var apkInstall = new ApkInstall(Application.persistentDataPath + "/Apk/MyPAL.apk");
		apkInstall.Install();
	}
}
