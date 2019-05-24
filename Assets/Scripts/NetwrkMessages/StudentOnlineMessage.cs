using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using UnityEngine;
using _UI;

public class StudentOnlineMessage : INetworkMessage {	

	public void Send(NetworkingPlayer player, Binary frame, NetWorker sender)
	{
		Debug.Log(string.Format("student online with ip of: {0}", frame.Sender.Ip));
                
		var studentModel = ByteToObject.ConvertToObject<StudentModel>(frame.StreamData.CompressBytes());
		MasterListController.StudentOnline(studentModel);
		Debug.Log(string.Format("online {0}", studentModel.Lastname));

		MainNetwork.Instance.OnlineStudents[frame.Sender.Ip] = studentModel;
	}
}
