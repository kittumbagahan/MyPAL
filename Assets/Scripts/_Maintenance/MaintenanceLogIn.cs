using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//MaintenanceForgotPassword.cs
//MaintenanceDevResetPWDWindow.cs
public class MaintenanceLogIn : MonoBehaviour {
	[SerializeField]
	InputField inputPWD;
	[SerializeField]
	GameObject canvas;
	[SerializeField]
	MaintenanceForgotPassword forgotPWDKey;
	void Start()
	{
		//PlayerPrefs.SetString("admin","1234");
		//txtPWD.text = "";
		FirstRun();
	}


	public void LogIn()
	{
		if(PlayerPrefs.GetString("admin") == inputPWD.text){
			print("Log In Success");
			MaintenanceManager.ins.loggedInPassword = "admin";
			UserParentalManager.ins.SpawnParentControl();
			ClearPWDField();
			canvas.SetActive(false);

		}
		else if(inputPWD.text == "tammytam")
		{
			MessageBox.ins.ShowOk("THE SUPER USER HAS LOGGED IN!", MessageBox.MsgIcon.msgInformation, new UnityAction(ShowParentalControl));
			print("Developer Logged In!");
			MaintenanceManager.ins.loggedInPassword = "tammytam";
			ClearPWDField();
			canvas.SetActive(false);

		}
		else if(ResetPWD())
		{
			PlayerPrefs.SetString("admin","1234");
			MessageBox.ins.ShowOk("Password has been reset to 1234.",MessageBox.MsgIcon.msgInformation, null);
		}
		else
		{
			MessageBox.ins.ShowOk("Access denied.",MessageBox.MsgIcon.msgError, null);
		}
	}

	public void ClearPWDField()
	{
		//txtPWD.text = "";
		inputPWD.text = "";
	}

	void FirstRun()
	{
		if(PlayerPrefs.GetInt("Maintenance_first_run") != 1)
		{
			PlayerPrefs.SetString("admin","1234");
			PlayerPrefs.SetInt("Maintenance_first_run", 1);

		}
	}

	public void ContactUs()
	{
		MessageBox.ins.ShowOk("Please email us at palabaydev@gmail.com",MessageBox.MsgIcon.msgInformation, null);
	}

	void ShowParentalControl()
	{
		UserParentalManager.ins.SpawnParentControl();
	}

	bool ResetPWD()
	{
		for(int i=0; i<forgotPWDKey.pwdResetCode.Length; i++)
		{
			if(inputPWD.text.Equals(forgotPWDKey.pwdResetCode[i]) && PlayerPrefs.GetInt(forgotPWDKey.pwdResetCode[i]) != 1)
			{
				//forgotPWDKey.forgotPWDcode = forgotPWDKey.pwdResetCode[i];
				PlayerPrefs.SetInt(forgotPWDKey.pwdResetCode[i], 1);
				return true;
			}
		}

		return false;
	}
}
