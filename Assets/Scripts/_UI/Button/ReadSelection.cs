using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ReadSelection : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	ReadType readType;

	private Dictionary<ReadType, string> _readType = new Dictionary<ReadType, string>();

	private void Awake()
	{
		_readType.Add(ReadType.AutoRead, "Auto Read");
		_readType.Add(ReadType.ReadItToMe, "Read It To Me");
		_readType.Add(ReadType.ReadItMySelf, "Read It My Self");
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		StudentOnlineActivity();

		StoryBookStart.instance.Read(readType);
	}

	private void StudentOnlineActivity()
	{
		DataService.Open();
		var studentModel = DataService.StudentModel(StoryBookSaveManager.ins.activeUser_id);
		DataService.Close();

		var networkActivity = new NetworkActivity();
		networkActivity.Activity =
			string.Format("{0} : {1}", 
				StoryBookSaveManager.ins.selectedBook.ToString().Replace('_', ' '), 
				_readType[readType]);
		networkActivity.StudentModel = studentModel;

		MainNetwork.Instance.StudentOnlineActivity(networkActivity);
	}

	#endregion
}
