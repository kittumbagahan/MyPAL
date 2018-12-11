using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;


public sealed class Launcher : MonoBehaviour {

    [SerializeField]
    LauncherNetworking lNet;

    int findingServerTime = 0;
    void Start()
    {
        //check for open network
        //if there is open network connect\
        lNet.Initialize();
       
        lNet.OnFindingServer += FindingServer;
        lNet.OnConnectedToServer += OnConnected;
        lNet.FindServer();
      
    }

    private void OnConnected()
    {
        Debug.Log("I am connected");
        //wait for the server to send download url
        //automatically accept
        //download assetbundle from url
        //on download complete load the bundle
    }

    private void FindingServer()
    {
        

        findingServerTime += 1;
        Debug.Log(string.Format("finding server counting={0}", findingServerTime));

        if(findingServerTime >= 60)
        {
            Debug.Log("What to do?");
        }
    }
}
