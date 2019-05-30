using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using UnityEngine;
using _UI;

public class StudentOnlineActivity : INetworkMessage {
    public void Send(NetworkingPlayer player, Binary frame, NetWorker sender)
    {
        var networkActivity = ByteToObject.ConvertTo<NetworkActivity>(frame.StreamData.CompressBytes());
        MasterListController.StudentOnlineActivity(networkActivity.StudentModel, networkActivity.Activity);
        Debug.Log(string.Format("online {0}, activity {1}", networkActivity.StudentModel.Lastname, networkActivity.Activity));
    }
}
