using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

public class MasterListController : MonoBehaviour
{
	private Dictionary<int, StudentModel> _studentModels;
	
	// Use this for initialization
	void Start ()
	{		
		SetUp();
	}

	private void SetUp()
	{
		_studentModels = new Dictionary<int, StudentModel>();

		Subscribe();
	}

	private void Subscribe()
	{
		ServerView.OnOpenMasterList = OpenMasterList;
		ServerView.OnCloseMasterList = CloseMasterList;
	}

	private void OpenMasterList()
	{
		CreateStudentBlock();
	}
	
	private void CloseMasterList()
	{
		
	}

	private void CreateStudentBlock()
	{
		var serverView = GetComponent<ServerView>();
		
		DataService.Open();
		IEnumerable<StudentModel> studentModels = DataService.GetStudents();
		foreach (var studentModel in studentModels)
		{						
			var studentBlock = Instantiate(serverView.StudentBlock, serverView.Parent.transform).GetComponent<StudentBlock>();
			studentBlock.StudentModel = studentModel;
			studentBlock.SetViewOffline();
			
			_studentModels.Add(studentModel.Id, studentModel);
		}

		DataService.Close();
	}

}
