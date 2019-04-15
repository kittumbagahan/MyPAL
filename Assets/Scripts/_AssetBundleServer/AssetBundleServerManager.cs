using UnityEngine;
using UnityEngine.UI;

namespace _AssetBundleServer
{
    public class AssetBundleServerManager : MonoBehaviour
    {
        [SerializeField] private Button btnOK, btnStart;

        public InputField fieldURL;
        public InputField fieldVersion;

        private int numberOfConnectedClients;

        [SerializeField] private AssetBundleServerNetwork serverNet;

        [SerializeField] private Text txtConnectionInfo;

        [SerializeField] private Text txtNumOfConnection;

        private void Start()
        {
            serverNet.OnClientAccepted += IncNumberConnectedClients;
            serverNet.OnClientDisconnected += DecNumberConnectedClients; //currently not working
            InvokeRepeating("NetworkState", 1f, 1f);
            btnStart.interactable = false;
            btnOK.onClick.AddListener(AssetBundleInfoReady);
        }

        private void AssetBundleInfoReady()
        {
            if ("".Equals(fieldURL.text) || "".Equals(fieldVersion.text))
                MessageBox.ins.ShowOk("All fields are required.", MessageBox.MsgIcon.msgError, null);
            else
                MessageBox.ins.ShowQuestion("Are you sure?\nURL " + fieldURL.text + "\nVersion " + fieldVersion.text,
                    MessageBox.MsgIcon.msgInformation,
                    StartServerYes, StartServerNo);
        }


        private void IncNumberConnectedClients()
        {
            numberOfConnectedClients++;
            txtNumOfConnection.text = "Number of connected clients: " + numberOfConnectedClients;
        }

        private void DecNumberConnectedClients()
        {
            numberOfConnectedClients--;
            txtNumOfConnection.text = "Number of connected clients: " + numberOfConnectedClients;
        }

        private void StartServerYes()
        {
            btnStart.interactable = true;
            fieldURL.interactable = false;
            fieldVersion.interactable = false;
            btnOK.interactable = false;
        }

        private void StartServerNo()
        {
        }

        private void NetworkState()
        {
            txtConnectionInfo.text =
                Application.internetReachability == NetworkReachability.NotReachable ? "Not connected" : "Connected";
            
            txtConnectionInfo.color =
                Application.internetReachability == NetworkReachability.NotReachable ? Color.red : Color.green;
                        
        }
    }
}