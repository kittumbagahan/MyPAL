using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour {

    [SerializeField]
    Button _btnHome, _btnDataImport, _btnSendDb, _btnReceiveDb;

	// Use this for initialization
	void Start () {
        _btnHome.onClick.AddListener (Home);
        _btnDataImport.onClick.AddListener (DataImport);
        _btnSendDb.onClick.AddListener (SendDb);
        _btnReceiveDb.onClick.AddListener (ReceiveDb);
	}

    void Home()
    {        
        LoadScene("BookShelf");
    }

    void DataImport()
    {             
        LoadScene("DataExporter");
    }

    void SendDb()
    {     
        LoadScene("DBSyncSend");
    }

    void ReceiveDb()
    {        
        LoadScene("DbSyncReceive");
    }

    private void LoadScene(string sceneToLoad)
    {
        EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene("empty");
    }
}
