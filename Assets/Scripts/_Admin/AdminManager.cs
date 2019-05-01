using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour {

    [SerializeField]
    private Button 
        btnHome, 
        btnDataExport, 
        btnSendDb, 
        btnReceiveDb, 
        btnUpdateMyPal,
        btnresetVersion;

	// Use this for initialization
	void Start () {
        btnHome.onClick.AddListener (Home);
        btnDataExport.onClick.AddListener (DataImport);
        btnSendDb.onClick.AddListener (SendDb);
        btnReceiveDb.onClick.AddListener (ReceiveDb);
        btnUpdateMyPal.onClick.AddListener(UpdateMyPal);
        btnresetVersion.onClick.AddListener(ResetVersion);
    }

    private void ResetVersion()
    {
        MessageBox.ins.ShowQuestion("Reset current version?", 
            MessageBox.MsgIcon.msgInformation, 
            () => PlayerPrefs.DeleteKey("version"), 
            null);
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
        //EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
        //SceneManager.LoadScene("empty");
        SceneManager.LoadScene(sceneToLoad);
    }
}
