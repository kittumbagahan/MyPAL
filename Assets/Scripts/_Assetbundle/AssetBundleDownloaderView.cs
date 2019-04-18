using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace _Assetbundle
{
	public class AssetBundleDownloaderView : MonoBehaviour
	{
		[SerializeField] private Button connectButton;
		[SerializeField] private Button updateButton;

		public Button ConnectButton
		{
			get { return connectButton; }
		}

		public Button UpdateButton
		{
			get { return updateButton; }
		}
	}
}
