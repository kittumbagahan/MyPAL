using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentBlock : MonoBehaviour
{	
	private StudentModel _studentModel;

	[SerializeField] private Text text;

	private string _networkStatus = NetworkStatus.Offline.ToString();		
		
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
		var networkText = new NetworkText();
		text.text = networkText.SetNetworkView(_studentModel, NetworkStatus.Offline);
	}
	
	public void SetViewOnline()
	{
		var networkText = new NetworkText();
		text.text = networkText.SetNetworkView(_studentModel, NetworkStatus.Online);	
	}

	public void SetViewOnlineActivity(string activity)
	{
		var networkText = new NetworkText();
		text.text = networkText.SetNetworkViewActivity(_studentModel, activity);
	}
}

public enum NetworkStatus
{
	Offline = 0,
	Online = 1
}
