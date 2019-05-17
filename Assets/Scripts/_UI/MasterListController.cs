using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SQLite4Unity3d;
using UnityEngine;

public class MasterListController : MonoBehaviour
{
	private static Dictionary<int, StudentBlock> _students;
	
	// Use this for initialization
	void Start ()
	{		
		SetUp();
	}

	private void SetUp()
	{
		_students = new Dictionary<int, StudentBlock>();

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
			
			_students.Add(studentModel.Id, studentBlock);
		}

		DataService.Close();
	}

	public static void StudentOnline(StudentModel studentModel)
	{
		_students[studentModel.Id].SetViewOnline();
	}

	public static void StudentOnlineActivity(StudentModel studentModel, string activity)
	{
		
	}
}
