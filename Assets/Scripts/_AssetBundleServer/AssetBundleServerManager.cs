using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AssetBundleServerManager : MonoBehaviour {

    [SerializeField]
    AssetBundleServerNetwork serverNet;

    public InputField fieldURL;
    public InputField fieldVersion;
    [SerializeField]
    Text txtNumOfConnection;
    [SerializeField]
    Text txtConnectionInfo;

    int numberOfConnectedClients;

    private void Start()
    {
        serverNet.OnClientAccepted += IncNumberConnectedClients;
        InvokeRepeating("NetworkState", 1f, 1f);
    }

    public void StartServer()
    {
        MessageBox.ins.ShowQuestion("Are you sure?\nURL " + fieldURL.text + "\nVersion " + fieldVersion.text, MessageBox.MsgIcon.msgInformation,
            new UnityAction(StartServerYes), new UnityAction(StartServerNo));
    }


    void IncNumberConnectedClients()
    {
        numberOfConnectedClients++;
        txtNumOfConnection.text = "Number of connected clients: " + numberOfConnectedClients;
    } 

    void StartServerYes()
    {
        serverNet.Host();
    }
    void StartServerNo()
    {

    }

    void NetworkState()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            txtConnectionInfo.text = "Not connected";
            txtConnectionInfo.color = Color.red;
        }
        else
        {
            txtConnectionInfo.text = "Connected";
            txtConnectionInfo.color = Color.green;
        }
    }
}
