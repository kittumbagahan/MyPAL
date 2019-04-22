using UnityEngine;
using UnityEngine.UI;

public class DownloadDialog : MonoBehaviour
{

	[SerializeField] private Image assetBundleProgress;
	[SerializeField] private Image totalAssetBundleProgress;

	[SerializeField] private Text assetBundleProgressText;
	[SerializeField] private Text totalAssetBundleProgressText;
	[SerializeField] private Text assetBeingDownloaded;
	
	public Image AssetBundleProgress
	{
		get { return assetBundleProgress; }
	}

	public Image TotalAssetBundleProgress
	{
		get { return totalAssetBundleProgress; }
	}

	public Text AssetBundleProgressText
	{
		get { return assetBundleProgressText; }
	}

	public Text TotalAssetBundleProgressText
	{
		get { return totalAssetBundleProgressText; }
	}

	public Text AssetBeingDownloaded
	{
		get { return assetBeingDownloaded; }
	}
}
