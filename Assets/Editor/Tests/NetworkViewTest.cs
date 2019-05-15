using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;

public class NetworkViewTest {

	[Test]
	public void NetworkText_SetViewOffline_ShouldShowStudentIsOffline()
	{
		StudentModel studentModel = new StudentModel()
		{
			Givenname = "Huehue",
			Middlename = "Ahuehue",
			Lastname = "Hahah"
		};
		
		var gameObject = new GameObject();
		var text = gameObject.AddComponent<Text>();

		text.supportRichText = true;
		
		var networkText = new NetworkText();

		text.text = networkText.SetNetworkView(studentModel, NetworkStatus.Offline);		
		
		Debug.Log(text.text);
	}	
	
	[Test]
	public void NetworkText_SetViewOnline_ShouldShowStudentIsOnline()
	{
		StudentModel studentModel = new StudentModel()
		{
			Givenname = "Huehue",
			Middlename = "Ahuehue",
			Lastname = "Hahah"
		};
		
		var gameObject = new GameObject();
		var text = gameObject.AddComponent<Text>();

		text.supportRichText = true;
		
		var networkText = new NetworkText();

		text.text = networkText.SetNetworkView(studentModel, NetworkStatus.Online);		
		
		Debug.Log(text.text);
	}

	[Test]
	public void NetworkText_SetViewActivity_ShouldShowActivity()
	{
		StudentModel studentModel = new StudentModel()
		{
			Givenname = "Huehue",
			Middlename = "Ahuehue",
			Lastname = "Hahah"
		};
		
		var gameObject = new GameObject();
		var text = gameObject.AddComponent<Text>();

		text.supportRichText = true;
		
		var networkText = new NetworkText();

		text.text = networkText.SetNetworkViewActivity(studentModel, "Testing, MyPAL");		
		
		Debug.Log(text.text);
	}
}
