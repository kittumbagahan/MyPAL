using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Assetbundle
{
	public class AssetBundleDownloader : MonoBehaviour
	{

		[SerializeField] private Text _appVersion;
		[SerializeField] private Text _appName;
	
		// Use this for initialization
		void Start () {
			GetApplicationName();
			GetVersion();
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		#region METHODS

		private void GetVersion()
		{
			_appVersion.text = PlayerPrefs.GetString("appVersion", "1.0.0");
		}

		private void GetApplicationName()
		{
			_appName.text = Application.productName;
		}


		IEnumerator DownloadAssetBundle()
		{
			var url = "";
			yield return null;
		}

		#endregion
	}
}
