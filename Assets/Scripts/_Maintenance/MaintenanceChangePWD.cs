using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class MaintenanceChangePWD : MonoBehaviour {

	[SerializeField]
	InputField inputOld, inputNew;



	public void ChangePassword()
	{
		if(inputOld.text.Equals("") || inputNew.text.Equals(""))
		{
			MessageBox.ins.ShowOk("All fields are required.", MessageBox.MsgIcon.msgError, null);
		}
		else
		{
			if(PlayerPrefs.GetString("admin").Equals(inputOld.text) && !inputNew.text.Equals("tammytam"))
			{
				if(!inputOld.text.Equals(inputNew.text))
				{
					PlayerPrefs.SetString("admin", inputNew.text);
					MessageBox.ins.ShowOk("Change password success!", MessageBox.MsgIcon.msgInformation, new UnityAction(CloseWindow));
					inputNew.text = "";
					inputOld.text = "";

				}
				else
				{
					MessageBox.ins.ShowOk("Old and new password must not be the same.", MessageBox.MsgIcon.msgError, null);
				}
			}
			else
			{
				MessageBox.ins.ShowOk("Wrong old password.", MessageBox.MsgIcon.msgError, null);
			}
		}

	}


	void CloseWindow()
	{
		gameObject.SetActive(false);
	}
}
