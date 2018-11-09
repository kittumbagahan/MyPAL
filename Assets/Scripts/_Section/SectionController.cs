using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SectionController : MonoBehaviour {
    public static SectionController ins;
    [SerializeField]
    GameObject panelSectionInput;
    [SerializeField]
    GameObject panelEditSectionInput;
    [SerializeField]
    GameObject btnSectionContainer;
    [SerializeField]
    GameObject btnSectionPrefab;
   
    [SerializeField]
    int currentMaxSection = 0;
    [SerializeField]
    int maxSectionAllowed;



    public bool editMode = false;

    void Start () {
        if(ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
        }
        if (PlayerPrefs.GetInt("maxNumberOfSectionsAllowed") == 0) {
            PlayerPrefs.SetInt("maxNumberOfSectionsAllowed", 3);
        }
        //PlayerPrefs.SetInt("maxNumberOfSectionsAllowed", 10);
        LoadSections();
	}
	
    public void LoadSections()
    {
        int n = 0;
        maxSectionAllowed = PlayerPrefs.GetInt("maxNumberOfSectionsAllowed");

        for (int i = 0; i < btnSectionContainer.transform.childCount; i++)
        {
            Destroy(btnSectionContainer.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < maxSectionAllowed; i++)
        {
            if (PlayerPrefs.GetString("section_id" + i) != "")
            {
                GameObject _obj = Instantiate(btnSectionPrefab);
                Section _section = _obj.GetComponent<Section>();
                _section.id = i;
                _section.name = PlayerPrefs.GetString("section_id" + i);
                if(_obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>() == null)
                {
                    _obj.transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
                }
                _obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _section.name;
                _obj.transform.SetParent(btnSectionContainer.transform);
                n++;
            }
        }

        currentMaxSection = n;
    }

	public void CreateNewSection(Text newSection)
    {
        if ("".Equals(newSection.text))
        {
            MessageBox.ins.ShowOk("Enter section name.", MessageBox.MsgIcon.msgError, null);
        }
        else
        {
            if (currentMaxSection < maxSectionAllowed)
            {
                if (!IsDuplicate(newSection.text))
                {
                    int newId = SetSectionId();
                    PlayerPrefs.SetString("section_id" + newId, newSection.text);
                    GameObject _obj = Instantiate(btnSectionPrefab);
                    Section _section = _obj.GetComponent<Section>();
                    _section.id = newId;
                    _section.name = newSection.text;
                    _obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _section.name;
                    _obj.transform.SetParent(btnSectionContainer.transform);

                    panelSectionInput.gameObject.SetActive(false);
                    currentMaxSection++;
                }
                else
                {
                    MessageBox.ins.ShowOk(newSection.text + " already exist.", MessageBox.MsgIcon.msgError, null);
                }
            }
            else
            {
                MessageBox.ins.ShowOk("Max number of sections allowed already reached.", MessageBox.MsgIcon.msgError, null);
            }
        }
        //PrintSections();
    }

    int SectionSelectMax()
    {
        currentMaxSection = 0;
        int n = 0;
        while (PlayerPrefs.GetString("section_id" + n) != "")
        {
            n++;
        }
        currentMaxSection = n;
        return n;
    }

    int SetSectionId()
    {
        for (int i = 0; i < maxSectionAllowed; i++)
        {
            if (PlayerPrefs.GetString("section_id" + i.ToString()) == "")
            {
                print("GIVING " + i);
                return i;
            }
        }
        return 0;
    }

    bool IsDuplicate(string s)
    {
        int n = 0;
        while (PlayerPrefs.GetString("section_id" + n) != "")
        {
            if (PlayerPrefs.GetString("section_id" + n) == s)
            {
                return true;
            }
            n++;
        }
        return false;
    }

    void PrintSections()
    {
        int n = 0;
        while (PlayerPrefs.GetString("section_id" + n) != "")
        {
            print("section: " + PlayerPrefs.GetString("section_id" + n));
            n++;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    //Edit-----------------------------
    public void EditSection()
    {
        editMode = true;
        MessageBox.ins.ShowOkCancel("Select section to edit. Click cancel to return.", MessageBox.MsgIcon.msgInformation,
            EditYes, EditCancel);
    }

    void EditYes()
    {
        editMode = true;

    }
    void EditCancel()
    {
        editMode = false;
        MessageBox.ins.ShowOk("Edit section cancelled.", MessageBox.MsgIcon.msgInformation, null);
    }

   void EditClose()
   {
      editMode = false;
      EditSectionView view = panelEditSectionInput.GetComponent<EditSectionView> ();
      view.btnOK.onClick.RemoveAllListeners ();
   }

   public void Edit(Section s)
    {
        EditSectionView view = panelEditSectionInput.GetComponent<EditSectionView>();
        view.gameObject.SetActive(true);
        view.txtSectionName.text = s.name;

        UpdateSection updateSection = new UpdateSection(view, s);
        view.btnOK.onClick.AddListener(updateSection.UpdateSectionName);
        view.btnClose.onClick.AddListener (EditClose);
       
    }
}

class UpdateSection{
    EditSectionView view;
    Section s;
    public UpdateSection(EditSectionView view, Section s)
    {
        this.view = view;
        this.s = s;
    }

    public void UpdateSectionName()
    {
        if ("".Equals(view.txtSectionName.text))
        {
            MessageBox.ins.ShowOk("All fields are required.", MessageBox.MsgIcon.msgError, null);
        }

        else if (view.txtSectionName.text.Equals(s.name))
        {
            //nothing to update just say updated!
            MessageBox.ins.ShowOk("Section name updated!", MessageBox.MsgIcon.msgInformation, null);
            SectionController.ins.editMode = false;
            view.btnOK.onClick.RemoveAllListeners ();
            //view.btnClose.onClick.RemoveAllListeners ();
        }
        else
        {
            PlayerPrefs.SetString("section_id" + s.id, view.txtSectionName.text);
            SectionController.ins.LoadSections();
            MessageBox.ins.ShowOk("Section name updated!", MessageBox.MsgIcon.msgInformation, null);
            SectionController.ins.editMode = false;
            view.btnOK.onClick.RemoveAllListeners ();
            //view.btnClose.onClick.RemoveAllListeners ();
      }
    }
}
