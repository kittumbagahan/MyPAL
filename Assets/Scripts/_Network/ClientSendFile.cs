using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System.IO;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using SQLite4Unity3d;
using System;
using System.Text;
using LitJson;
using UnityEngine.Events;
using _Assetbundle;
using _AssetBundleServer;
using _UI;
using _Version;
using Text = UnityEngine.UI.Text;

public class ClientSendFile : MonoBehaviour
{
    [SerializeField] private DownloadDialog _downloadDialog;
    
    public enum MessageGroup
    {
        Insert = 2,
        Update = 3,
        Book_UpdateReadCount = 4,
        Book_UpdateReadToMeCount = 5,
        Book_UpdateAutoReadCount = 6,
        Sync = 7,
        AssetBundle = 8,
        CSV = 9,
        FullSync = 10,
        StudentOffline = 11,
        StudentOnline = 12,
        StudentOnlineActivity = 13
    }
    
    private readonly Dictionary<int, INetworkMessage> _messages = new Dictionary<int, INetworkMessage>();
    
    int sentCount = 0; // should only be 2, for section and admin
    int currentCount = 0;   
    
    //DataService dataService;

    private void Start()
    {        
        GenerateMessages();
    }

    private void GenerateMessages()
    {
        SetDataBaseSyncMessage();
        SetAssetBundleMessage();
        SetStudentMessage();
        SetDataBaseMessage();
    }

    private void SetStudentMessage()
    {
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.StudentOffline,
            new StudentOfflineMessage());
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.StudentOnline, new StudentOnlineMessage());
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.StudentOnlineActivity,
            new StudentOnlineActivity());
    }

    private void SetDataBaseSyncMessage()
    {
        var sync = new SyncMessage();
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.Sync, sync);
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.FullSync, sync);
    }

    private void SetAssetBundleMessage()
    {
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.AssetBundle,
            new AssetBundleMessage(_downloadDialog));
    }

    private void SetDataBaseMessage()
    {
        var dataBaseMessage = new DataBaseMessage();
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.Insert, dataBaseMessage);
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.Update, dataBaseMessage);
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.Book_UpdateReadCount, dataBaseMessage);
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.Book_UpdateAutoReadCount, dataBaseMessage);
        _messages.Add(MessageGroupIds.START_OF_GENERIC_IDS + (int) MessageGroup.Book_UpdateReadToMeCount, dataBaseMessage);
    }

    private void OnEnable()
    {
        NetworkManager.Instance.Networker.binaryMessageReceived += ReceiveFile;
    }

    private void OnDisable()
    {
        NetworkManager.Instance.Networker.binaryMessageReceived -= ReceiveFile;
    }

    private void ReceiveFile(NetworkingPlayer player, Binary frame, NetWorker sender)
    {
        Debug.Log("frame group id:" + frame.GroupId);
        Debug.Log("Message group id of sync, " + MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Sync);

        MainThreadManager.Run(() =>
        {
            try
            {
                _messages[frame.GroupId].Send(player, frame, sender);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);                
            }            
        });     
    }                 

    public void SendData(NetworkData pNetworkData, MessageGroup messageGroup)
    {       
        // Throw an error if this is not the server
        var networker = NetworkManager.Instance.Networker;

        // event when file is sent        

        if (networker.IsServer)
        {
            Debug.LogError("Only the client can send files in this example!");
            return;
        }      

		byte[] allData = { };


		// convert pData as byte[]
		BinaryFormatter binFormatter = new BinaryFormatter();
		MemoryStream memStream = new MemoryStream ();
		binFormatter.Serialize (memStream, pNetworkData);

		allData = memStream.ToArray ();

		Debug.Log ("allData " + allData.Length);		

//        // Prepare a byte array for sending
//        BMSByte allData = new BMSByte();        
//
//        // Add the file name to the start of the payload        
//        ObjectMapper.Instance.MapBytes(allData);        

        // Send the file to all connected clients
        Binary frame = new Binary(
            networker.Time.Timestep,                    // The current timestep for this frame
            false,                                      // We are server, no mask needed
            allData,                                    // The file that is being sent
            Receivers.Others,                           // Send to all clients
            MessageGroupIds.START_OF_GENERIC_IDS + (int)messageGroup,   // Some random fake number
            networker is TCPServer);

//        if (networker is UDPServer)
//            ((UDPServer)networker).Send(frame, true);
//        else
//            ((TCPServer)networker).SendAll(frame);

		if (networker is UDPClient)
			((UDPClient)networker).Send (frame, true);
		else
			((TCPClient)networker).Send (frame);
		
						
        //StringBuilder("sending file");
    }

    public void SendCSV(string pCSV)
    {
        // Throw an error if this is not the server
        var networker = NetworkManager.Instance.Networker;

        // event when file is sent        

        if (networker.IsServer)
        {
            Debug.LogError("Only the client can send files in this example!");
            return;
        }

        byte[] allData = { };        

        allData = Encoding.UTF8.GetBytes(pCSV);        
        Debug.Log("string length " + pCSV.Length);
        Debug.Log("allData " + allData.Length);

        //        // Prepare a byte array for sending
        //        BMSByte allData = new BMSByte();        
        //
        //        // Add the file name to the start of the payload        
        //        ObjectMapper.Instance.MapBytes(allData);        

        // Send the file to all connected clients
        Binary frame = new Binary(
            networker.Time.Timestep,                    // The current timestep for this frame
            false,                                      // We are server, no mask needed
            allData,                                    // The file that is being sent
            Receivers.Others,                           // Send to all clients
            MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.CSV,   // Some random fake number
            networker is TCPServer);

        //        if (networker is UDPServer)
        //            ((UDPServer)networker).Send(frame, true);
        //        else
        //            ((TCPServer)networker).SendAll(frame);

        if (networker is UDPClient)
            ((UDPClient)networker).Send(frame, true);
        else
            ((TCPClient)networker).Send(frame);

        Debug.Log("CSV done");
        GetComponent<DataImportNetwork>().DataSent();
        //StringBuilder("sending file");
    }

    public void SendDatabase(string pFilePath, ClientSendFile.MessageGroup pMessageGroup)
    {
        // test
        //MessageBox.ins.ShowOk (string.Format ("File path is {0}", pFilePath), MessageBox.MsgIcon.msgInformation, null);
        //return;
        Debug.Log ("File Path " + pFilePath);

        // kit, temp
        //sentFile = true;

        // pass file path value to private variable
         string filePath = pFilePath;

        // Throw an error if this is not the server
        var networker = NetworkManager.Instance.Networker;

        // event when file is sent        

        if (!networker.IsServer)
        {
            Debug.LogError ("Only the server can send files in this example!");
            return;
        }

        // Throw an error if the file does not exist
        if (!File.Exists (filePath))
        {
            Debug.LogError ("The file " + filePath + " could not be found");
            return;
        }

        // Prepare a byte array for sending
        BMSByte allData = new BMSByte ();

        // Add the file name to the start of the payload
        ObjectMapper.Instance.MapBytes (allData, Path.GetFileName (filePath));

        // Add the data to the payload
        allData.Append (File.ReadAllBytes (filePath));

        // Send the file to all connected clients
        Binary frame = new Binary (
            networker.Time.Timestep,                    // The current timestep for this frame
            false,                                      // We are server, no mask needed
            allData,                                    // The file that is being sent
            Receivers.Others,                           // Send to all clients
            MessageGroupIds.START_OF_GENERIC_IDS + (int)pMessageGroup,   // Some random fake number
            networker is TCPServer);

        if (networker is UDPServer)
            ((UDPServer)networker).Send (frame, true);
        else
            ((TCPServer)networker).SendAll (frame);        
    }

    public void SendStudentOnline(StudentModel studentModel)
    {
        // Throw an error if this is not the server
        var networker = NetworkManager.Instance.Networker;       
        
        // event when file is sent        

        if (networker.IsServer)
        {
            Debug.LogError("Only the client can send files in this example!");
            return;
        }      

        byte[] allData = { };


        // convert pData as byte[]
        BinaryFormatter binFormatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream ();
        binFormatter.Serialize (memStream, studentModel);

        allData = memStream.ToArray ();

        Debug.Log ("allData " + allData.Length);		

//        // Prepare a byte array for sending
//        BMSByte allData = new BMSByte();        
//
//        // Add the file name to the start of the payload        
//        ObjectMapper.Instance.MapBytes(allData);        

        // Send the file to all connected clients
        Binary frame = new Binary(
            networker.Time.Timestep,                    // The current timestep for this frame
            false,                                      // We are server, no mask needed
            allData,                                    // The file that is being sent
            Receivers.Others,                           // Send to all clients
            MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.StudentOnline,   // Some random fake number
            networker is TCPServer);

//        if (networker is UDPServer)
//            ((UDPServer)networker).Send(frame, true);
//        else
//            ((TCPServer)networker).SendAll(frame);

        if (networker is UDPClient)
            ((UDPClient)networker).Send (frame, true);
        else
            ((TCPClient)networker).Send (frame);
		
						
        //StringBuilder("sending file");
    }
    
    public void SendStudentOffline(StudentModel studentModel)
    {
        // Throw an error if this is not the server
        var networker = NetworkManager.Instance.Networker;

        // event when file is sent        

        if (networker.IsServer)
        {
            Debug.LogError("Only the client can send files in this example!");
            return;
        }      

        byte[] allData = { };


        // convert pData as byte[]
        BinaryFormatter binFormatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream ();
        binFormatter.Serialize (memStream, studentModel);

        allData = memStream.ToArray ();

        Debug.Log ("allData " + allData.Length);		

//        // Prepare a byte array for sending
//        BMSByte allData = new BMSByte();        
//
//        // Add the file name to the start of the payload        
//        ObjectMapper.Instance.MapBytes(allData);        

        // Send the file to all connected clients
        Binary frame = new Binary(
            networker.Time.Timestep,                    // The current timestep for this frame
            false,                                      // We are server, no mask needed
            allData,                                    // The file that is being sent
            Receivers.Others,                           // Send to all clients
            MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.StudentOffline,   // Some random fake number
            networker is TCPServer);

//        if (networker is UDPServer)
//            ((UDPServer)networker).Send(frame, true);
//        else
//            ((TCPServer)networker).SendAll(frame);

        if (networker is UDPClient)
            ((UDPClient)networker).Send (frame, true);
        else
            ((TCPClient)networker).Send (frame);
		
						
        //StringBuilder("sending file");
    }
    
    public void SendStudentOnlineActivity(NetworkActivity networkActivity)
    {
        // Throw an error if this is not the server
        var networker = NetworkManager.Instance.Networker;

        // event when file is sent        

        if (networker.IsServer)
        {
            Debug.LogError("Only the client can send files in this example!");
            return;
        }      

        byte[] allData = { };


        // convert pData as byte[]
        BinaryFormatter binFormatter = new BinaryFormatter();
        MemoryStream memStream = new MemoryStream ();
        binFormatter.Serialize (memStream, networkActivity);

        allData = memStream.ToArray ();

        Debug.Log ("allData " + allData.Length);		

        Binary frame = new Binary(
            networker.Time.Timestep,                    // The current timestep for this frame
            false,                                      // We are server, no mask needed
            allData,                                    // The file that is being sent
            Receivers.Others,                           // Send to all clients
            MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.StudentOnlineActivity,   // Some random fake number
            networker is TCPServer);

        if (networker is UDPClient)
            ((UDPClient)networker).Send (frame, true);
        else
            ((TCPClient)networker).Send (frame);
								
        //StringBuilder("sending file");
    }

    bool CreateStudentBookModel (NetworkData pNetworkData)
    {
        // check student book model
        DataService.Open();
        StudentBookModel studentModel = DataService._connection.Table<StudentBookModel> ().Where
        (
           x => x.SectionId == pNetworkData.studentBook_SectionId &&
           x.StudentId == pNetworkData.studentBook_StudentId &&
           x.BookId == pNetworkData.studentBook_bookId
        ).FirstOrDefault ();

        if (studentModel == null)
        {
            Debug.Log ("Create student book model");
            StudentBookModel studentBookModel = new StudentBookModel
            {
                SectionId = pNetworkData.studentBook_SectionId,
                StudentId = pNetworkData.studentBook_StudentId,
                BookId = pNetworkData.studentBook_bookId,
                ReadCount = pNetworkData.studentBook_readCount,
                ReadToMeCount = pNetworkData.studentBook_readToMeCount,
                AutoReadCount = pNetworkData.studentBook_autoReadCount
            };
            DataService._connection.Insert (studentBookModel);

            DataService.Close();
            return true;            
        }
        else
        {
            Debug.Log ("Create student book model update");
            DataService.Close();
            return false;
        }
    }   
}
