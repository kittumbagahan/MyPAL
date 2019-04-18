using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour {

    [SerializeField]
    private Button btnHome, btnDataExport, btnSendDb, btnReceiveDb, btnUpdateMyPal;

	// Use this for initialization
	void Start () {
        btnHome.onClick.AddListener (Home);
        btnDataExport.onClick.AddListener (DataImport);
        btnSendDb.onClick.AddListener (SendDb);
        btnReceiveDb.onClick.AddListener (ReceiveDb);
        btnUpdateMyPal.onClick.AddListener(UpdateMyPal);
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
    
    void UpdateMyPal()
    {        
//        LoadScene("UpdateMyPAL");
        SceneManager.LoadScene("UpdateMyPAL");
    }
    
    private void LoadScene(string sceneToLoad)
    {
        EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene("empty");
    }
}
