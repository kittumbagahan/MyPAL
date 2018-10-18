using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class MaintenanceNumberOfStudentsAllowed : MonoBehaviour {

    [SerializeField]
    InputField txtNumOfStudentsAllowed;
    int maxStudentAllowed;
  
    
    void OnEnable () {
        maxStudentAllowed = PlayerPrefs.GetInt("maxNumberOfStudentsAllowed");
        txtNumOfStudentsAllowed.text = maxStudentAllowed.ToString();
    }	

    public void UpdateNumberOfAllowedStudents()
    {
        try
        {
            int input = int.Parse(txtNumOfStudentsAllowed.text);
            if (input == maxStudentAllowed)
            {
                MessageBox.ins.ShowOk("Enter new number!", MessageBox.MsgIcon.msgError, null);
            }
            else if (input < 10)
            {
                MessageBox.ins.ShowOk("Minimum number of student is 10.", MessageBox.MsgIcon.msgError, null);
            }
            else
            {
                MessageBox.ins.ShowQuestion("Are you sure you want to make changes?", MessageBox.MsgIcon.msgWarning, new UnityAction(Yes), null);
            }
        }
        catch(Exception ex)
        {
            MessageBox.ins.ShowOk(ex.ToString(), MessageBox.MsgIcon.msgError, null);
        }
    }
	
    void Yes()
    {
        maxStudentAllowed = int.Parse(txtNumOfStudentsAllowed.text);
        PlayerPrefs.SetInt("maxNumberOfStudentsAllowed", maxStudentAllowed);
        MessageBox.ins.ShowOk("Number of students per section updated!", MessageBox.MsgIcon.msgInformation, null);
    }
}
