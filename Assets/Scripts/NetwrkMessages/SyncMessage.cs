using System.Collections;
using System.Collections.Generic;
using System.IO;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using UnityEngine;

public class SyncMessage: INetworkMessage
{
    private int _sentCount = 0;
    private int _currentCount = 0;
	public void Send(NetworkingPlayer player, Binary frame, NetWorker sender)
	{		
            Debug.LogError("Reading file! Sync");

            //StringBuilder("Reading file!");        

            // Read the string from the beginning of the payload
            string fileName = frame.StreamData.GetBasicType<string>();

            Debug.LogError("sync file name " + fileName);

            Debug.Log("File name is " + fileName + ", path: " + Application.persistentDataPath);

            try
            {                
                if(frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)ClientSendFile.MessageGroup.Sync)
                    DataSync(frame, fileName);
                else                    
                    DataFullSync(frame, fileName);                                     
            }

            catch (IOException ex)
            {
                Debug.LogError("file exception! " + ex.Message);
            }              
	}

    private void DataFullSync(Binary frame, string fileName)
    {
        if (fileName == "admin.db")
        {
            AdminFullSync(frame, fileName);
        }
        // section db
        else
        {
            SectionFullSync(frame, fileName);
        }

        if (_sentCount == _currentCount)
        {
            GameObject.FindObjectOfType<DbSyncNetwork>().UpdateClientDB();
            Debug.Log("All DB sent!");
            MessageBox.ins.ShowOk("Database download successful!", MessageBox.MsgIcon.msgInformation, null);
        }
    }

    private void SectionFullSync(Binary frame, string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            File.Delete(Application.persistentDataPath + "/" + fileName);
        }

        // Write the rest of the payload as the contents of the file and
        // use the file name that was extracted as the file's name 
        File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath, fileName),
            frame.StreamData.CompressBytes());
        _currentCount++;
    }

    private void AdminFullSync(Binary frame, string fileName)
    {
        Admin(frame, fileName);

        DataService.Open("system/admin.db");
        _sentCount = DataService._connection.Table<AdminSectionsModel>().Count();
        Debug.Log("Client section count: " + _sentCount + ", current count: " + _currentCount);
        DataService.Close();
    }

    private static void Admin(Binary frame, string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/system/" + fileName))
        {
            File.Delete(Application.persistentDataPath + "/system/" + fileName);
        }

        // Write the rest of the payload as the contents of the file and
        // use the file name that was extracted as the file's name 
        File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath + "/system", fileName),
            frame.StreamData.CompressBytes());
    }

    private void DataSync(Binary frame, string fileName)
    {
        if (fileName == "admin.db")
        {
            AdminSync(frame, fileName);
        }
        // section db
        else
        {
            SectionSync(frame, fileName);
        }

        // check sent count
        _sentCount++;
        if (_sentCount == 2)
        {
            MainNetwork.Instance.LoadSectionSelection();
            _sentCount = 0;
        }
    }

    private void SectionSync(Binary frame, string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            File.Delete(Application.persistentDataPath + "/" + fileName);
        }

        // Write the rest of the payload as the contents of the file and
        // use the file name that was extracted as the file's name 
        File.WriteAllBytes(string.Format("{0}/{1}", Application.persistentDataPath, fileName),
            frame.StreamData.CompressBytes());

        StoryBookSaveManager.ins.activeSection = Path.GetFileNameWithoutExtension(fileName);
    }

    private void AdminSync(Binary frame, string fileName)
    {
        Admin(frame, fileName);
    }
}
