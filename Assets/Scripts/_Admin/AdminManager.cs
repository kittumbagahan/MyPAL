using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour {

    [SerializeField]
    Button btnHome, btnDataImport, btnSendDB, btnReceiveDB;

	// Use this for initialization
	void Start () {
        btnHome.onClick.AddListener (Home);
        btnDataImport.onClick.AddListener (DataImport);
        btnSendDB.onClick.AddListener (SendDB);
        btnReceiveDB.onClick.AddListener (ReceiveDB);
	}

    void Home()
    {
        SceneManager.LoadScene ("BookShelf");
    }

    void DataImport()
    {
        Debug.Log("Loading from ADmin Manager " + gameObject.name );
        SceneManager.LoadScene("DataImporter");
    }

    void SendDB()
    {
        SceneManager.LoadScene ("DBSyncSend");
    }

    void ReceiveDB()
    {
        SceneManager.LoadScene ("DbSYncReceive");
    }
}
