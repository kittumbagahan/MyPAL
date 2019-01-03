using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine;

public class MaintenanceNumberOfSectionsAllowed : MonoBehaviour {

    [SerializeField]
    InputField txtNumOfSectionsAllowed;
    int maxSectionAllowed;

    NumberOfSectionsModel model = null;
    void OnEnable()
    {
       
        DataService.Open("admin.db");
        model = DataService._connection.Table<NumberOfSectionsModel>().Where(x => x.Id == 1).FirstOrDefault();
        maxSectionAllowed = model.MaxSection;
        txtNumOfSectionsAllowed.text = maxSectionAllowed.ToString();
        DataService.Close();
    }

    public void UpdateNumberOfAllowedSections()
    {
        try
        {
            int input = int.Parse(txtNumOfSectionsAllowed.text);
            if (input == maxSectionAllowed)
            {
                MessageBox.ins.ShowOk("Enter new number!", MessageBox.MsgIcon.msgError, null);
            }
            else if (input < maxSectionAllowed)
            {
                MessageBox.ins.ShowOk("Minimum number of section is 3.", MessageBox.MsgIcon.msgError, null);
            }
            else
            {
                MessageBox.ins.ShowQuestion("Are you sure you want to make changes?", MessageBox.MsgIcon.msgWarning, new UnityAction(Yes), null);
            }
        }
        catch (Exception ex)
        {
            MessageBox.ins.ShowOk(ex.ToString(), MessageBox.MsgIcon.msgError, null);
        }
    }

    void Yes()
    {
        //DataService ds = new DataService ();
        DataService.Open("admin.db");
        maxSectionAllowed = int.Parse(txtNumOfSectionsAllowed.text);
        model.MaxSection = maxSectionAllowed;
        DataService._connection.Update(model);
 
        MessageBox.ins.ShowOk("Number of sections updated!", MessageBox.MsgIcon.msgInformation, null);
        DataService.Close();
    }
}
