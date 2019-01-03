using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

public class SectionController : MonoBehaviour
{
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
    GameObject btnEdit;
    [SerializeField]
    int currentMaxSection = 0;
    [SerializeField]
    int maxSectionAllowed;



    public bool editMode = false;

    void Start()
    {
        if (ins != null)
        {
          
        }
        else
        {
            ins = this;
        }
        //if (PlayerPrefs.GetInt("maxNumberOfSectionsAllowed") == 0)
        //{
        //    PlayerPrefs.SetInt("maxNumberOfSectionsAllowed", 3);
        //}
        //maxSectionAllowed = PlayerPrefs.GetInt("maxNumberOfSectionsAllowed");

        DataService.Open("admin.db");
        maxSectionAllowed = DataService._connection.Table<NumberOfSectionsModel>().FirstOrDefault().MaxSection;
        DataService.Close();
    }

    void OnEnable()
    {
        //Debug.Log("SECCTIONS NOT LOADed");
        LoadSectionsSQL();
    }

    public void LoadSectionsSQL()
    {
        //DataService ds = new DataService();
        DataService.Open("admin.db");

        var sections = DataService._connection.Table<AdminSectionsModel>();

        for (int i = 0; i < btnSectionContainer.transform.childCount; i++)
        {
            Destroy(btnSectionContainer.transform.GetChild(i).gameObject);
        }

        foreach (var section in sections)
        {
            GameObject _obj = Instantiate(btnSectionPrefab);
            Section _section = _obj.GetComponent<Section>();
            _section.UID = section.DeviceId;
            _section.id = section.SectionId;
            _section.name = section.Description;
            if (_obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>() == null)
            {
                _obj.transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
            }
            _obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _section.name + _section.id;
            _obj.transform.SetParent(btnSectionContainer.transform);
            currentMaxSection++;
        }

        if (btnSectionContainer.transform.childCount == 0)
        {
            btnEdit.gameObject.SetActive(false);
        }
        else
        {
            if (UserRestrictionController.ins.restriction == 0)
            {
                btnEdit.gameObject.SetActive(true);
            }

        }

        DataService.Close();
    }

    public void CreateNewSection(Text newSection)
    {
        //create section for this device
        if ("".Equals(newSection.text))
        {
            MessageBox.ins.ShowOk("Enter section name.", MessageBox.MsgIcon.msgError, null);
        }
        else
        {

            if (currentMaxSection < maxSectionAllowed)
            {
                bool dup = false;
                for (int i = 0; i < btnSectionContainer.transform.childCount; i++)
                {
                    Debug.Log(btnSectionContainer.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text);
                    if (newSection.text.Equals(btnSectionContainer.transform.GetChild(i)
                       .gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text))
                    {
                        dup = true;
                    }

                }
                if (!dup)
                {
                    DatabaseSectionController dsc = new DatabaseSectionController();

                    //PlayerPrefs.SetString("activeDatabase", newSection.text + ".db");
                    //create the section database
                    dsc.CreateSectionDb(newSection.text + ".db");
                    Debug.Log("section db file created!");
                    //create the section database tables
                    dsc.CreateSectionTables(newSection.text + ".db");
                    Debug.Log("section db tables created!");

                    //insert new section in admin database
                    DataService.Open("admin.db");
                    AdminSectionsModel asm = new AdminSectionsModel
                    {
                        DeviceId = SystemInfo.deviceUniqueIdentifier,
                        SectionId = 1,
                        Description = newSection.text
                    };
                    DataService._connection.Insert(asm);
                    DataService.Close();
                    Debug.Log("section added into admin sections");

                    //create section in section databse
                    DataService.Open(newSection.text + ".db");

                    SectionModel model = new SectionModel { DeviceId = SystemInfo.deviceUniqueIdentifier, Description = newSection.text };
                    DataService._connection.Insert(model);

                    GameObject _obj = Instantiate(btnSectionPrefab);
                    Section _section = _obj.GetComponent<Section>();
                    SectionModel s = DataService._connection.Table<SectionModel>().Where(x => x.Description == model.Description).FirstOrDefault();
                    _section.id = s.Id;
                    _section.name = newSection.text;
                    _obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _section.name;
                    _obj.transform.SetParent(btnSectionContainer.transform);

                    panelSectionInput.gameObject.SetActive(false);
                    currentMaxSection++;

                    DataService.Close();
                    Debug.Log("section created into section db!");
                   
                }
                else
                {
                    MessageBox.ins.ShowOk(newSection.text + " already exist.", MessageBox.MsgIcon.msgError, null);
                }

                /** OLD
                DataService.Open();

                if (DataService._connection.Table<SectionModel>().Where(x => x.Description == newSection.text).FirstOrDefault() == null)
                {

                    SectionModel model = new SectionModel { DeviceId = SystemInfo.deviceUniqueIdentifier, Description = newSection.text };
                    DataService._connection.Insert(model);

                    //int newId = SetSectionId ();
                    //PlayerPrefs.SetString ("section_id" + newId, newSection.text);
                    GameObject _obj = Instantiate(btnSectionPrefab);
                    Section _section = _obj.GetComponent<Section>();
                    SectionModel s = DataService._connection.Table<SectionModel>().Where(x => x.Description == model.Description).FirstOrDefault();
                    _section.id = s.Id;
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

                DataService.Close();
                */
            }
            else
            {
                MessageBox.ins.ShowOk("Max number of sections allowed already reached.", MessageBox.MsgIcon.msgError, null);
            }
        }
        //PrintSections();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    //Edit-----------------------------
    public void EditSection()
    {
        if (btnSectionContainer.transform.childCount == 0)
        {
            MessageBox.ins.ShowOk("No section to edit.", MessageBox.MsgIcon.msgInformation,
               null);
        }
        else
        {
            editMode = true;
            MessageBox.ins.ShowOkCancel("Select section to edit. Click cancel to return.", MessageBox.MsgIcon.msgInformation,
                EditYes, EditCancel);
        }

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
        EditSectionView view = panelEditSectionInput.GetComponent<EditSectionView>();
        view.btnOK.onClick.RemoveAllListeners();

    }

    public void Edit(Section s)
    {
        EditSectionView view = panelEditSectionInput.GetComponent<EditSectionView>();
        view.gameObject.SetActive(true);
        view.txtSectionName.text = s.name;

        UpdateSection updateSection = new UpdateSection(view, s);
        view.btnOK.onClick.AddListener(updateSection.UpdateSectionName);
        view.btnClose.onClick.AddListener(EditClose);

    }
}

class UpdateSection
{
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

            view.btnOK.onClick.RemoveAllListeners();
            //view.btnClose.onClick.RemoveAllListeners ();
        }
        else
        {
            //Rename section database
            DatabaseSectionController dsc = new DatabaseSectionController();
            dsc.RenameDb(s.name + ".db", view.txtSectionName.text + ".db");

            //update section in admin database
            DataService.Open("admin.db");
            AdminSectionsModel asm = DataService._connection.Table<AdminSectionsModel>().Where(x=>x.Description == s.name).FirstOrDefault();
            asm.Description = view.txtSectionName.text;
            DataService._connection.Update(asm);
            DataService.Close();

            DataService.Open(view.txtSectionName.text + ".db");
            SectionModel model = new SectionModel
            {
                Id = s.id,
                Description = view.txtSectionName.text
            };
            //_connection.Execute ("Update UserTable set currentCar=" + currnetCarNumb + " where
            //ID = "+userID);
            DataService._connection.Execute("Update SectionModel set Description='" + model.Description + "' where Id='" + model.Id + "'");
            MessageBox.ins.ShowOk("Section name updated!", MessageBox.MsgIcon.msgInformation, null);
            SectionController.ins.editMode = false;
            SectionController.ins.LoadSectionsSQL();
            view.btnOK.onClick.RemoveAllListeners();

            DataService.Close();

            //view.btnClose.onClick.RemoveAllListeners ();
        }
    }
}
