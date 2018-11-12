using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System.IO;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class ClientSendFile : MonoBehaviour
{   
    private void Start()
    {        
        NetworkManager.Instance.Networker.binaryMessageReceived += ReceiveFile;
    }
    
    private void ReceiveFile(NetworkingPlayer player, Binary frame, NetWorker sender)
    {
        // We are looking to read a very specific message
        if (frame.GroupId != MessageGroupIds.START_OF_GENERIC_IDS + 2)
            return;

        Debug.Log("Reading file!");                        

		Debug.Log (string.Format ("Stream data {0}", frame.StreamData.CompressBytes ()));

		NetworkTestObject netObj = ConvertToObject(frame.StreamData.CompressBytes());

		Debug.Log (string.Format ("Object. Name: {0}, age: {1}", netObj.name, netObj.age));

		// kit, test data display text
		MainThreadManager.Run( () => GameObject.FindGameObjectWithTag("data").GetComponent<UnityEngine.UI.Text>().text = string.Format("Name: {0}\nAge: {1}\nSection: {2}\n\n", netObj.name, netObj.age, netObj.section));

        // Write the rest of the payload as the contents of the file and
        // use the file name that was extracted as the file's name    

        //MainThreadManager.Run(() => File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath, fileName), frame.StreamData.CompressBytes()));        
    }

    public void SendData(NetworkTestObject pData)
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
		binFormatter.Serialize (memStream, pData);

		allData = memStream.ToArray ();

		Debug.Log ("allData " + allData.Length);

		// kit, test
		NetworkTestObject _obj = new NetworkTestObject();
		_obj = ConvertToObject (allData);
		Debug.Log (string.Format ("Test. Name {0}, age {1}.", _obj.name, _obj.age));

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
            MessageGroupIds.START_OF_GENERIC_IDS + 2,   // Some random fake number
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

	NetworkTestObject ConvertToObject(byte[] byteData)
	{
		BinaryFormatter bin = new BinaryFormatter ();
		MemoryStream ms = new MemoryStream ();
		ms.Write (byteData, 0, byteData.Length);
		ms.Seek (0, SeekOrigin.Begin);

		return (NetworkTestObject)bin.Deserialize (ms);
	}
}
