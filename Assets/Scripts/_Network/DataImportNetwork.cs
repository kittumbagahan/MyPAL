using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Lobby;
using BeardedManStudios.SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

using System.Net.Sockets;
using SQLite4Unity3d;
using System;
using System.Text;

public class DataImportNetwork : MonoBehaviour
{

    //public InputField ipAddress = null;
    //public InputField portNumber = null;
    public bool DontChangeSceneOnConnect = false;
    public string masterServerHost = string.Empty;
    public ushort masterServerPort = 12940;
    public string natServerHost = string.Empty;
    public ushort natServerPort = 12941;
    public bool connectUsingMatchmaking = false;
    public bool useElo = false;
    public int myElo = 0;
    public int eloRequired = 0;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button backButton;
    
    public GameObject networkManager = null;
    private NetworkManager mgr = null;
    private NetWorker server, client;

    //private List<Button> _uiButtons = new List<Button>();
    private bool _matchmaking = false;
    public bool useMainThreadManagerForRPCs = true;
    public bool useInlineChat = false;

    public bool getLocalNetworkConnections = false;

    public bool useTCP = false;

    // kit
    [SerializeField]
    private ushort mPort = 12937;
    private string mIpAddress = "127.0.0.1";
    [SerializeField] Button btnReceiver, btnSender, btnSendData;
    [SerializeField]
    Text txtStat;

    ClientSendFile mClientSendFile;

    // kit
    public static DataImportNetwork Instance;
    NetworkingPlayer player;

    [SerializeField]
    Dropdown dropdownSection;

    private void Start ()
    {
        //ipAddress.text = "127.0.0.1";
        //portNumber.text = "15937";          

        //for (int i = 0; i < ToggledButtons.Length; ++i)
        //{
        //  Button btn = ToggledButtons[i].GetComponent<Button>();
        //  if (btn != null)
        //      _uiButtons.Add(btn);
        //}                                                                    
        
        mClientSendFile = GetComponent<ClientSendFile> ();
        
        NavigationSetup();


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy (gameObject);
        }

        if (!useTCP)
        {
            // Do any firewall opening requests on the operating system
            //NetWorker.PingForFirewall(ushort.Parse(portNumber.text));
            NetWorker.PingForFirewall (mPort);
        }

        if (useMainThreadManagerForRPCs)
            Rpc.MainThreadRunner = MainThreadManager.Instance;

        if (getLocalNetworkConnections)
        {
            NetWorker.localServerLocated += LocalServerLocated;
            //NetWorker.RefreshLocalUdpListings(ushort.Parse(portNumber.text));
            NetWorker.RefreshLocalUdpListings (mPort);
        }

        RetrieveData();
    }

    private void NavigationSetup()
    {
        var navigation = new Navigation(Quit);
        homeButton.onClick.AddListener(navigation.LoadBookShelf);
        backButton.onClick.AddListener(navigation.Back);
    }

    private void LocalServerLocated (NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        Debug.Log ("Found endpoint: " + endpoint.Address + ":" + endpoint.Port);
    }

    public void Connect ()
    {
        //ushort port;
        //if(!ushort.TryParse(portNumber.text, out port))
        //{
        //  Debug.LogError("The supplied port number is not within the allowed range 0-" + ushort.MaxValue);
        //      return;
        //}

        //NetWorker client;

        if (useTCP)
        {
            client = new TCPClient ();

            //((TCPClient)client).Connect(ipAddress.text, (ushort)port);
            ((TCPClient)client).Connect (mIpAddress, mPort);
            ((TCPClient)client).Connect (mIpAddress, mPort);
        }
        else
        {
            client = new UDPClient ();


            if (natServerHost.Trim ().Length == 0)
                //((UDPClient)client).Connect(ipAddress.text, (ushort)port);
                ((UDPClient)client).Connect (mIpAddress, mPort);
            else
                //((UDPClient)client).Connect(ipAddress.text, (ushort)port, natServerHost, natServerPort);
                ((UDPClient)client).Connect (mIpAddress, mPort, natServerHost, natServerPort);
        }

        // kit, add event                     
        client.serverAccepted += Client_serverAccepted;
        client.disconnected += Client_disconnected;

        Connected (client);
    }

    private void Client_disconnected (NetWorker sender)
    {
        Debug.Log (string.Format ("{0} is disconnected from server", "Huehue"));
        MainThreadManager.Run (ResetNetwork);
    }

    private void Client_serverAccepted (NetWorker sender)
    {
        Debug.Log (string.Format ("{0} is connected to server", "Huehue"));
        MainThreadManager.Run(() =>
        {
            txtStat.text = "connected";
            btnSendData.interactable = true;
        });
        //MainThreadManager.Run(() => SceneManager.LoadScene("Test"));
        //MainThreadManager.Run(() => btnStudent.GetComponent<StudentLogIn>().LogIn());
    }

    public void SendData()
    {
        MainThreadManager.Run(() =>
        {
            if (textExport == null || textExport == "")
                return;

            clientSendFile.SendCSV(textExport);
        });
    }

    public void Host ()
    {
        if (useTCP)
        {
            server = new TCPServer (100);

            ((TCPServer)server).Connect ();
        }
        else
        {
            server = new UDPServer (100);

            if (natServerHost.Trim ().Length == 0)
            //((UDPServer)server).Connect(ipAddress.text, ushort.Parse(portNumber.text));
            {
                try
                {
                    ((UDPServer)server).Connect (mIpAddress, mPort);
                }
                catch (BaseNetworkException ex)
                {
                    MessageBox.ins.ShowOk ("A server already exist. Please check your network connection.", MessageBox.MsgIcon.msgInformation, null);
                }
            }
            else
            //((UDPServer)server).Connect(port: ushort.Parse(portNumber.text), natHost: natServerHost, natPort: natServerPort);
            {
                try
                {
                    ((UDPServer)server).Connect (port: mPort, natHost: natServerHost, natPort: natServerPort);
                }
                catch (BaseNetworkException ex)
                {
                    MessageBox.ins.ShowOk ("A server already exist. Please check your network connection.", MessageBox.MsgIcon.msgInformation, null);
                }
            }
        }

        server.playerTimeout += (player, sender) =>
        {
            Debug.Log ("Player " + player.NetworkId + " timed out");
        };
        //LobbyService.Instance.Initialize(server);
        server.playerConnected += Server_playerConnected;
        server.disconnected += Server_disconnected;
        server.playerAccepted += Server_playerAccepted;
        // kit, event                      
        Connected (server);
    }

    private void Server_playerAccepted (NetworkingPlayer player, NetWorker sender)
    {
        Debug.Log ("player is accepted");
        //clientSendFile.SendDatabase (Application.persistentDataPath + "/" + DataService.DbName ());
    }

    private void Server_disconnected (NetWorker sender)
    {
        Debug.Log ("Server disconnected");
        ResetNetwork ();
        Reset ();
    }

    private void Server_playerConnected (NetworkingPlayer player, NetWorker sender)
    {
        Debug.Log ("Player connected");
        // send latest db to client
        //clientSendFile.SendDatabase(Application.persistentDataPath + "/" + DataService.DbName());
    }

    private void TestLocalServerFind (NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        Debug.Log ("Address: " + endpoint.Address + ", Port: " + endpoint.Port + ", Server? " + endpoint.IsServer);

        // a server is found
        mPort = endpoint.Port;
        mIpAddress = endpoint.Address;

        isServerFound = true;
        // stop coroutine for finding a server
        //StopCoroutine(_FindServer());
    }

    public void Connected (NetWorker networker)
    {
        if (!networker.IsBound)
        {
            Debug.LogError ("NetWorker failed to bind");
            return;
        }

        if (mgr == null && networkManager == null)
        {
            Debug.LogWarning ("A network manager was not provided, generating a new one instead");
            networkManager = new GameObject ("Network Manager");
            mgr = networkManager.AddComponent<NetworkManager> ();
        }
        else if (mgr == null)
            mgr = Instantiate (networkManager).GetComponent<NetworkManager> ();

        // If we are using the master server we need to get the registration data
        JSONNode masterServerData = null;
        if (!string.IsNullOrEmpty (masterServerHost))
        {
            string serverId = "myGame";
            string serverName = "Forge Game";
            string type = "Deathmatch";
            string mode = "Teams";
            string comment = "Demo comment...";

            masterServerData = mgr.MasterServerRegisterData (networker, serverId, serverName, type, mode, comment, useElo, eloRequired);
        }

        mgr.Initialize (networker, masterServerHost, masterServerPort, masterServerData);

        if (useInlineChat && networker.IsServer)
            SceneManager.sceneLoaded += CreateInlineChat;

        if (networker is IServer)
        {
            // kit, is server
            btnReceiver.GetComponentInChildren<TextMeshProUGUI> ().text = "Disconnect";
            btnReceiver.onClick.RemoveAllListeners ();
            btnReceiver.onClick.AddListener (Quit);
            Debug.Log ("Connected as server");
        }

        mClientSendFile.enabled = true;
    }

    private void CreateInlineChat (Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= CreateInlineChat;
        var chat = NetworkManager.Instance.InstantiateChatManager ();
        DontDestroyOnLoad (chat.gameObject);
    }

    private void OnApplicationQuit ()
    {
        Quit ();
    }

    void Quit ()
    {
        Debug.Log ("Quit");

        if (getLocalNetworkConnections)
            NetWorker.EndSession ();

        //if (server != null) server.Disconnect(true);
        if (server != null) server.Disconnect (false);
    }

    public ClientSendFile clientSendFile
    {
        get { return mClientSendFile; }
    }

    #region ADDITIONAL LOGIC
    bool isServerFound = false;
    private void Reset ()
    {
        isServerFound = false;
        mIpAddress = "";
    }

    private void ResetNetwork ()
    {
        if (btnReceiver != null)
        {
            btnReceiver.interactable = true;
            btnReceiver.onClick.RemoveAllListeners ();
            btnReceiver.onClick.AddListener (AsReceiver);
        }

        if (btnSender != null)
        {
            btnSender.onClick.RemoveAllListeners ();
            btnSender.onClick.AddListener (AsSender);
        }

        if (btnSender != null)
            btnSender.GetComponentInChildren<Text> ().text = "Connect";
        if (btnReceiver != null)
            btnReceiver.GetComponentInChildren<Text> ().text = "Connect";

        if (txtStat != null)
            txtStat.text = "";

        if (btnSendData != null)
            btnSendData.interactable = false;
    }

    private void OnEnable ()
    {
        // find teacher and student button

        if (btnReceiver != null)
        {
            btnReceiver.onClick.RemoveAllListeners ();
            btnReceiver.onClick.AddListener (AsReceiver);
        }
        if (btnSender != null)
        {
            btnSender.onClick.RemoveAllListeners ();
            btnSender.onClick.AddListener (AsSender);
        }

        NetWorker.localServerLocated += TestLocalServerFind;

        Debug.Log (btnReceiver);
        Debug.Log (btnSender);
    }

    private void OnDisable ()
    {
        // reset
        Reset ();
        NetWorker.localServerLocated -= TestLocalServerFind;
    }

    public void AsReceiver ()
    {
        // the one who will send the data        
        Host ();
    }

    public void AsSender ()
    {
        if(btnReceiver != null)
            btnReceiver.GetComponent<Button>().interactable = false;
        if(btnSender != null)
            btnSender.GetComponentInChildren<Text>().text = "Disconnect";

        btnSender.onClick.RemoveAllListeners();
        btnSender.onClick.AddListener(() =>
        {
            StopCoroutine("_FindServer");
            ResetNetwork();
            StopCoroutine("_FindServerLoading");
        });

        StartCoroutine("_FindServer");
        StartCoroutine("_FindServerLoading");
    }

    WaitForSeconds wfs = new WaitForSeconds (1.5f);
    IEnumerator _FindServer ()
    {
        while (isServerFound == false)
        {
            NetWorker.RefreshLocalUdpListings (mPort);
            Debug.Log ("Finding Server");
            yield return wfs;
        }

        Debug.Log ("Coroutine exited.");
        Connect ();

    }

    IEnumerator _FindServerLoading ()
    {
        int counter = 0;
        string status = "";
        while (isServerFound == false)
        {
            switch (counter)
            {
                case 0:
                status = "Connecting.";
                break;
                case 1:
                status = "Connecting..";
                break;
                case 2:
                status = "Connecting...";
                break;
            }

            if (counter == 2)
                counter = 0;
            else
                counter++;

            // stat
            txtStat.text = status;

            yield return new WaitForSeconds (1);
        }
    }

    #endregion

    AccuracyABC accuracyABC;
    AccuracyAfterTheRain accuracyAfterTheRain;
    AccuracyChatWithCat accuracyChatWithCat;
    AccuracyColorsAllMixedUp accuracyColorsAllMixedUp;
    AccuracyFavoriteBox accuracyFavoriteBox;
    AccuracyJoeyGoesToSchool accuracyJoeyGoesToSchool;
    AccuracySoundsFantastic accuracySoundsFantastic;
    AccuracyTinaAndJun accuracyTinaAndJun;
    AccuracyWhatDidYouSee accuracyWhatDidYouSee;
    AccuracyYummyShapes accuracyYummyShapes;

    string columns = "Fullname,Word,Observation,Total";
    string data;
    string textExport;

    void RetrieveData()
    {
        Debug.Log("Retrieve sections");
        DataService.Open("system/admin.db");
        SQLiteCommand command = DataService._connection.CreateCommand("select * from AdminSectionsModel");
        List<AdminSectionsModel> lstSections = new List<AdminSectionsModel>();
        lstSections = command.ExecuteQuery<AdminSectionsModel>();
        DataService.Close();

        dropdownSection.interactable = lstSections.Count > 0 ? true : false;

        if (lstSections.Count <= 0)
            return;

        List<string> options = new List<string>();

        for (int i = 0; i < lstSections.Count; i++)
        {
            Debug.Log(string.Format("Section {0}", lstSections[i].Description));
            options.Add(lstSections[i].Description);
        }

        dropdownSection.AddOptions(options);

        dropdownSection.onValueChanged.AddListener(ShowData);

        ShowData(0);
    }

    // test
    public void ShowData(int value)
    {
        textExport = "";
        columns = "Fullname,Word,Observation,Total";
        data = "";

        dropdownSection.interactable = false;
        if (btnSender != null)
            btnSender.interactable = false;

        string selectedSection = dropdownSection.options[value].text;

        if (selectedSection == "" || selectedSection == null)
            return;        

        // data        
        columns += Environment.NewLine + "," + Environment.NewLine;

        Debug.Log("Test");
        Debug.Log(dropdownSection.options[dropdownSection.value].text);

        DataService.Open(selectedSection + ".db");

        // student model                
        SQLiteCommand command = DataService._connection.CreateCommand("select * from StudentModel");// Order By Gender Desc
        List<StudentModel> students = command.ExecuteQuery<StudentModel>();
        // book model
        var book = DataService._connection.Table<BookModel>();

        for(int i = 0; i < students.Count; i++)
        {
            List<BookGrade> bookGradeList = new List<BookGrade>();

            foreach (var b in book)
            {
                BookGrade _bf = new BookGrade(b, students[i]);
                bookGradeList.Add(_bf);
                ModuleGrade wordGrade = new ModuleGrade();
                ModuleGrade observationGrade = new ModuleGrade();
                //Debug.Log(b.Description);

                int ID = students[i].Id;

                var activityModelWord = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == b.Id && x.Module == "WORD");
                foreach (var act in activityModelWord)
                {                    

                    //Debug.Log(act.Description);
                    //var grades = DataService._connection.Table<StudentActivityModel>().Where(x => x.StudentId == students[i].Id && x.SectionId == students[i].SectionId && x.ActivityId == act.Id);
                    var grades = DataService._connection.Table<StudentActivityModel>().Where(x => x.StudentId == ID && x.SectionId == 1 && x.ActivityId == act.Id);
                    foreach (var g in grades)
                    {
                        wordGrade.Add(g.Grade);
                        //Debug.Log(g.Grade);
                    }
                }

                var activityModelObservation = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == b.Id && x.Module == "OBSERVATION");
                foreach (var act in activityModelObservation)
                {
                    //Debug.Log(act.Description);
                    //var grades = DataService._connection.Table<StudentActivityModel>().Where(x => x.StudentId == students[i].Id && x.SectionId == students[i].SectionId && x.ActivityId == act.Id);
                    var grades = DataService._connection.Table<StudentActivityModel>().Where(x => x.StudentId == ID && x.SectionId == 1 && x.ActivityId == act.Id);
                    foreach (var g in grades)
                    {
                        observationGrade.Add(g.Grade);
                        //Debug.Log(g.Grade);
                    }
                }

                _bf.wordGrade = wordGrade;
                _bf.observationGrade = observationGrade;

            }

            double wordTotalGrade = 0;// = bookGradeList.Sum(x => x.wordGrade.GetAccuracy());
            double observationTotalGrade = 0;// = bookGradeList.Sum(x => x.observationGrade.GetAccuracy());

            foreach (var bg in bookGradeList)
            {
                wordTotalGrade += bg.wordGrade.GetAccuracy();
                observationTotalGrade += bg.observationGrade.GetAccuracy();
            }
            Debug.Log("Wordy!" + wordTotalGrade);                                    

            if (IsIncomplete(bookGradeList))
            {

                data += string.Format("\"{0}, {1} {2}.\",", students[i].Lastname, students[i].Givenname, students[i].Middlename) +
             string.Format("{0:0.00} inc,", wordTotalGrade) + string.Format("{0:0.00} inc,", observationTotalGrade) + string.Format("{0:0.00} inc", (wordTotalGrade + observationTotalGrade) / 2);
            }
            else
            {
                data += string.Format("\"{0}, {1} {2}.\",", students[i].Lastname, students[i].Givenname, students[i].Middlename) +
             string.Format("{0:0.00},", wordTotalGrade) + string.Format("{0:0.00},", observationTotalGrade) + string.Format("{0:0.00}", (wordTotalGrade + observationTotalGrade) / 2);
            }

            // check if last item, add new line if not
            if (students.Count - 1 != i)
            {
                data += Environment.NewLine;
            }                                                    
        }
        DataService.Close();
        //for (int i = 0; i < studentModel.Count; i++)
        //{            
        //    double wordTotalGrade = TotalWordGrade(studentModel[i].Id);
        //    double observationTotalGrade = TotalObservationGrade(studentModel[i].Id);

        //    Debug.Log("name " + studentModel[i].Lastname + ", id " + studentModel[i].Id);

        //    if (studentModel.Count - 1 == i)
        //        data += string.Format("\"{0}, {1} {2}.\"", studentModel[i].Lastname, studentModel[i].Givenname, studentModel[i].Middlename) + "," + wordTotalGrade + "," + observationTotalGrade +
        //        "," + (wordTotalGrade + observationTotalGrade);
        //    else
        //        data += string.Format("\"{0}, {1} {2}.\"", studentModel[i].Lastname, studentModel[i].Givenname, studentModel[i].Middlename) + "," + wordTotalGrade + "," + observationTotalGrade +
        //        "," + (wordTotalGrade + observationTotalGrade) + Environment.NewLine;
        //}

        Debug.Log("Huehue");
        textExport = columns + data;
        Debug.Log(textExport);

        dropdownSection.interactable = true;
        if (btnSender != null)
            btnSender.interactable = true;
    }

    StringBuilder SetStringLen(string s, int len = 50)
    {
        StringBuilder sb = new StringBuilder(s);
        int strLen = sb.Length;
        while (strLen++ < len)
        {
            sb.Append(" ");
        }

        return sb;
    }

    bool IsIncomplete(List<BookGrade> bg)
    {

        foreach (BookGrade g in bg)
        {
            if (g.wordGrade.GetAccuracy() == 0 || g.observationGrade.GetAccuracy() == 0)
            {
                return true;
            }
        }
        return false;
    }    

    // UI    

    public void DataSent()
    {
        MessageBox.ins.ShowOk("Data Uploaded.", MessageBox.MsgIcon.msgInformation, null);
        txtStat.text = "Data sent";
    }
}