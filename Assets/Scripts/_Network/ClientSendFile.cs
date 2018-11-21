﻿using BeardedManStudios;
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
        Update = 3,
        Book_UpdateReadCount = 4,
        Book_UpdateReadToMeCount = 5,
        Book_UpdateAutoReadCount = 6
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
        Debug.Log("Reading file!");

        // kit
        Debug.Log(string.Format("Insert group id {0}\nUpdate group id {1}", 
            (int)MessageGroup.Insert,
            (int)MessageGroup.Update));
        Debug.Log(string.Format("Message group {0}", frame.GroupId));

        NetworkData networkData = ConvertToObject(frame.StreamData.CompressBytes());
        // add to queue for execution
        networkQueue.Enqueue(networkData);

        // if message is insert
        if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Insert)
        {
            // kit
            Debug.Log("Insert");
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

            // kit
            Debug.Log(string.Format("ID {0}\nSection ID {1}\nStudent ID {2}\nBook ID {3}\nActivity ID {4}\nGrade {5}\nPlay Count {6}",
                studentActivityModel.Id,
                studentActivityModel.SectionId,
                studentActivityModel.StudentId,
                studentActivityModel.BookId,
                studentActivityModel.ActivityId,
                studentActivityModel.Grade,
                studentActivityModel.PlayCount));

            dataService._connection.Insert(studentActivityModel);
            networkQueue.Dequeue();
            
        }
        else
        {
            // handle update here
            string command = "";
            if(frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Update)
            {
                command = string.Format("Update StudentActivityModel set Grade='{0}'," +
                "PlayCount='{1}' where Id='{2}'", networkData.grade, networkData.playCount, networkData.ID);
            }            
            else if(frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateReadCount)
            {
                command = string.Format("Update StudentBookModel set ReadCount='{0}' where id='{1}'",
                    networkData.book_readCount,
                    networkData.book_Id);
            }
            else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateReadToMeCount)
            {
                command = string.Format("Update StudentBookModel set ReadToMeCount='{0}' where id='{1}'",
                    networkData.book_readToMeCount,
                    networkData.book_Id);
            }
            else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)MessageGroup.Book_UpdateAutoReadCount)
            {
                command = string.Format("Update StudentBookModel set AutoReadCount='{0}' where id='{1}'",
                    networkData.book_autoReadCount,
                    networkData.book_Id);
            }
            else
            {
                networkQueue.Dequeue();
                return;
            }

            // kit
            Debug.Log("Update");
            Debug.Log(command);

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
