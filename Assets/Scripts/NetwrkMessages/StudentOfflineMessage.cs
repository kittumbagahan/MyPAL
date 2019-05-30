using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using UnityEngine;
using _UI;

public class StudentOfflineMessage : INetworkMessage {
	
	public void Send(NetworkingPlayer player, Binary frame, NetWorker sender)
	{
		var studentModel = ByteToObject.ConvertTo<StudentModel>(frame.StreamData.CompressBytes());
		MasterListController.StudentOffline(studentModel);
		Debug.Log(string.Format("offline {0}", studentModel.Lastname)); 
	}
}
