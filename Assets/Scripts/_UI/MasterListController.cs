using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using UnityEngine;

namespace _UI
{
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
			ServerView.Instance.OnOpenMasterList = OpenMasterList;
			ServerView.Instance.OnCloseMasterList = CloseMasterList;
			
			ServerView.Instance.InputSearchStudent.onValueChanged.AddListener(SearchStudent);
		}

		private void SearchStudent(string value)
		{			
			foreach (var obj in _students)
			{
				if (value == null || value == "")
					obj.Value.gameObject.SetActive(true);
				else
					obj.Value.gameObject.SetActive(obj.Value.StudentModel.Lastname.ToLower().Contains(value.ToLower()));
			}		
		}

		private void OpenMasterList()
		{
			CreateStudentBlock();
		}
	
		private void CloseMasterList()
		{
			foreach (var student in _students)
			{
				DestroyObject(student.Value.gameObject);
			}
			
			_students.Clear();
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

		public static void StudentOffline(StudentModel studentModel)
		{
			_students[studentModel.Id].SetViewOffline();
		}
		
		public static void StudentOnline(StudentModel studentModel)
		{
			_students[studentModel.Id].SetViewOnline();
		}

		public static void StudentOnlineActivity(StudentModel studentModel, string activity)
		{
			_students[studentModel.Id].SetViewOnlineActivity(activity);
		}
	}
}
