using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using UnityEngine;

public class DataBaseMessage : INetworkMessage
{

    private readonly Queue<NetworkData> _networkQueue;
    
    public DataBaseMessage()
    {
        _networkQueue = new Queue<NetworkData>();
    }
	public void Send(NetworkingPlayer player, Binary frame, NetWorker sender)
	{
		Debug.Log("Reading file!");

                // kit
                Debug.Log(string.Format("Insert group id {0}\nUpdate group id {1}",
                    (int)ClientSendFile.MessageGroup.Insert,
                    (int)ClientSendFile.MessageGroup.Update));
                Debug.Log(string.Format("Message group {0}", frame.GroupId));

                NetworkData networkData = ByteToObject.ConvertTo<NetworkData>(frame.StreamData.CompressBytes());
                // add to queue for execution
                _networkQueue.Enqueue(networkData);

                // open db
                //MainThreadManager.Run(DataService.Open);
                while (_networkQueue.Count > 0)
                {
                    // kit
                    Debug.Log("Queue count " + _networkQueue.Count);

                    if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)ClientSendFile.MessageGroup.Update ||
                       frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)ClientSendFile.MessageGroup.Insert)
                    {
                        // activity model

                        string module = _networkQueue.Peek().activity_module;
                        string description = _networkQueue.Peek().activity_description;
                        int set = _networkQueue.Peek().activity_set;
                        string book_description = _networkQueue.Peek().book_description;

                        DataService.Open();

                        var activity = DataService._connection.Table<ActivityModel>().Where(x => x.Module == module &&
                                                                                        x.Description == description &&
                                                                                        x.Set == set).FirstOrDefault();

                        if (activity == null)
                        {
                            var _activity = new ActivityModel
                            {
                                BookId = DataService._connection.Table<BookModel>().Where(x => x.Description == book_description).FirstOrDefault().Id,
                                Description = _networkQueue.Peek().activity_description,
                                Module = _networkQueue.Peek().activity_module,
                                Set = _networkQueue.Peek().activity_set
                            };
                            DataService._connection.Insert(_activity);
                        }
                        DataService.Close();
                    }

                    // if message is insert
                    if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)ClientSendFile.MessageGroup.Insert)
                    {
                        // kit
                        Debug.Log("Insert");
                        // handle insert here, check first item in queue
                        StudentActivityModel studentActivityModel = new StudentActivityModel
                        {
                            Id = _networkQueue.Peek().studentActivity_ID,
                            SectionId = _networkQueue.Peek().studentActivity_sectionId,
                            StudentId = _networkQueue.Peek().studentActivity_studentId,
                            BookId = _networkQueue.Peek().studentActivity_bookId,
                            ActivityId = _networkQueue.Peek().studentActivity_activityId,
                            Grade = _networkQueue.Peek().studentActivity_grade,
                            PlayCount = _networkQueue.Peek().studentActivity_playCount
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

                        DataService.Open();
                        DataService._connection.Insert(studentActivityModel);
                        DataService.Close();

                        _networkQueue.Dequeue();

                    }
                    else
                    {
                        // handle update here
                        string command = "";
                        if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)ClientSendFile.MessageGroup.Update)
                        {
                            command = string.Format("Update StudentActivityModel set Grade='{0}'," +
                            "PlayCount='{1}' where Id='{2}'", networkData.studentActivity_grade, networkData.studentActivity_playCount, networkData.studentActivity_ID);
                        }
                        else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)ClientSendFile.MessageGroup.Book_UpdateReadCount)
                        {
                            Debug.Log("Update read count");
                            if (CreateStudentBookModel(_networkQueue.Peek()) == false)
                            {
                                command = string.Format("Update StudentBookModel set ReadCount='{0}' where id='{1}'",
                                    networkData.studentBook_readCount,
                                    networkData.studentBook_Id);

                                DataService.Open();
                                DataService._connection.Execute(command);
                                DataService.Close();
                            }
                        }
                        else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)ClientSendFile.MessageGroup.Book_UpdateReadToMeCount)
                        {
                            Debug.Log("Update read to me count");
                            if (CreateStudentBookModel(_networkQueue.Peek()) == false)
                            {
                                command = string.Format("Update StudentBookModel set ReadToMeCount='{0}' where id='{1}'",
                                networkData.studentBook_readToMeCount,
                                networkData.studentBook_Id);

                                DataService.Open();
                                DataService._connection.Execute(command);
                                DataService.Close();
                            }
                        }
                        else if (frame.GroupId == MessageGroupIds.START_OF_GENERIC_IDS + (int)ClientSendFile.MessageGroup.Book_UpdateAutoReadCount)
                        {
                            Debug.Log("Update auto read count");
                            if (CreateStudentBookModel(_networkQueue.Peek()) == false)
                            {
                                command = string.Format("Update StudentBookModel set AutoReadCount='{0}' where id='{1}'",
                                networkData.studentBook_autoReadCount,
                                networkData.studentBook_Id);

                                DataService.Open();
                                DataService._connection.Execute(command);
                                DataService.Close();
                            }
                        }

                        // kit
                        Debug.Log("Update");
                        Debug.Log(command);

                        //dataService._connection.Execute(command);
                        _networkQueue.Dequeue();
                    }
                }
                //MainThreadManager.Run(DataService.Close);
                // kit
                Debug.Log("Queue empty");
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
