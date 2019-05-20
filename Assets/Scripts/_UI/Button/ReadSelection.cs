using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ReadSelection : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	ReadType readType;


	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		DataService.Open();
		var studentModel = DataService.StudentModel(StoryBookSaveManager.ins.activeUser_id);
		DataService.Close();

		var networkActivity = new NetworkActivity();
		networkActivity.Activity = string.Format("{0} : {1}",StoryBookSaveManager.ins.selectedBook.ToString(), readType.ToString());
		
		StoryBookStart.instance.Read(readType);
	}
	#endregion
}
