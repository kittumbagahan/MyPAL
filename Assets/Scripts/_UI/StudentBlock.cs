using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentBlock : MonoBehaviour
{	
	private StudentModel _studentModel;

	[SerializeField] private Text text;

	private string networkStatus = NetworkStatus.Offline.ToString();		
		
	public StudentModel StudentModel
	{
		get { return _studentModel; }
		set { _studentModel = value; }
	}

	// Use this for initialization
	void Start ()
	{
		SetUp();
	}

	private void SetUp()
	{
		text = GetComponentInChildren<Text>();
	}

	public void SetViewOffline()
	{
		text.text = string.Format("{0}, {1} {2}. : {3}",
			_studentModel.Lastname,
			_studentModel.Givenname,
			_studentModel.Middlename,
			new Network().NetWorkStatus(NetworkStatus.Offline));
	}
}

public class Network
{
	public string NetWorkStatus(NetworkStatus status)
	{
		return status == NetworkStatus.Online ? "<color=#00ff00ff>Online</color>" : "<color=#ff0000ff>Offline</color>";
	}
}

public enum NetworkStatus
{
	Offline = 0,
	Online = 1
}
