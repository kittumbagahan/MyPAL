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

public class ClientSendFile : MonoBehaviour
{
    // kit
    [SerializeField]
    UnityEngine.UI.Text txtTest;

    public enum MessageGroup
    {
        Insert = 2,
        Update = 3
    }

    Queue<NetworkData> networkQueue;

    DataService dataService;

    private void Start()
    {        
        NetworkManager.Instance.Networker.binaryMessageReceived += ReceiveFile;
        networkQueue = new Queue<NetworkData>();

        // create database connection
        dataService = new DataService("tempDatabase.db");
    }
    
    private void ReceiveFile(NetworkingPlayer player, Binary frame, NetWorker sender)
    {

        if (frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Insert ||
            frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Update)
            return;

        Debug.Log("Reading file!");

        NetworkData networkData = ConvertToObject(frame.StreamData.CompressBytes());
        // add to queue for execution
        networkQueue.Enqueue(networkData);

        // if message is insert
        if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Insert)
        {
            // handle insert here, check first item in queue
            StudentActivityModel studentActivityModel = new StudentActivityModel
            {
                Id = networkQueue.Peek().ID,
                SectionId = networkQueue.Peek().sectionId,
                StudentId = networkQueue.Peek().studentId,
                BookId = networkQueue.Peek().bookId,
                ActivityId = networkQueue.Peek().activityId,
                Grade = networkQueue.Peek().grade,
                PlayCount = networkQueue.Peek().playCount
            };

            dataService._connection.Insert(studentActivityModel);
            networkQueue.Dequeue();
            
        }
        else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Update)
        {
            // handle update here
            string command = string.Format("Update StudentActivityModel set Grade='{0}'," +
                "PlayCount='{1}' where Id='{2}'", networkData.grade, networkData.playCount, networkData.ID);

            dataService._connection.Execute(command);
            networkQueue.Dequeue();
        }                                       		                

		// kit, test data display text
		//MainThreadManager.Run( () => GameObject.FindGameObjectWithTag("data").GetComponent<UnityEngine.UI.Text>().text = string.Format("Name: {0}\nAge: {1}\nSection: {2}\n\n", networkData.name, networkData.age, networkData.section));

        // Write the rest of the payload as the contents of the file and
        // use the file name that was extracted as the file's name    

        //MainThreadManager.Run(() => File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath, fileName), frame.StreamData.CompressBytes()));        
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
			Receivers.Server,                           // Send to all clients
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

	NetworkData ConvertToObject(byte[] byteData)
	{
		BinaryFormatter bin = new BinaryFormatter ();
		MemoryStream ms = new MemoryStream ();
		ms.Write (byteData, 0, byteData.Length);
		ms.Seek (0, SeekOrigin.Begin);

		return (NetworkData)bin.Deserialize (ms);
	}

    // kit, test
    void DebugText(string pText)
    {
        txtTest.text += pText;
    }
}
