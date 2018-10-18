using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class MaintenanceDevManageAdminWindow : MonoBehaviour {

	[SerializeField] InputField inputText;
	[SerializeField] Text txtAdminPwd;

	void OnEnable()
	{
		txtAdminPwd.text = "Admin password: " + PlayerPrefs.GetString("admin");	
	}

	public void ChangeAdminPWD()
	{
		if(!inputText.text.Equals(""))
		{
			MessageBox.ins.ShowQuestion("Change admin password?", MessageBox.MsgIcon.msgWarning, new UnityAction(Save), null);
		}
		else
		{
			MessageBox.ins.ShowOk("Enter new password!", MessageBox.MsgIcon.msgError, null);
		}


	}

	void Save()
	{
		PlayerPrefs.SetString("admin", inputText.text);
		MessageBox.ins.ShowOk("Admin password changed!", MessageBox.MsgIcon.msgInformation, null);
	}
}
