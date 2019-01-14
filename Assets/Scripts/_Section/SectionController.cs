using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using System;
using System.Linq;

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

      DataService.Open ("system/admin.db");
      maxSectionAllowed = DataService._connection.Table<NumberOfSectionsModel> ().FirstOrDefault ().MaxSection;
      DataService.Close ();
   }

   void OnEnable()
   {
      //Debug.Log("SECCTIONS NOT LOADed");
      Debug.Log ("Load Sections!");
      LoadSectionsSQL ();
   }

   public void LoadSectionsSQL()
   {
      //DataService ds = new DataService();
      DataService.Open ("system/admin.db");

      var sections = DataService._connection.Table<AdminSectionsModel> ();

      for (int i = 0; i < btnSectionContainer.transform.childCount; i++)
      {
         Destroy (btnSectionContainer.transform.GetChild (i).gameObject);
      }

      foreach (var section in sections)
      {
         GameObject _obj = Instantiate (btnSectionPrefab);
         Section _section = _obj.GetComponent<Section> ();
         _section.UID = section.DeviceId;
         _section.id = section.SectionId;
         _section.name = section.Description;
         _section.gradeLevel = section.GradeLevel;
         if (_obj.transform.GetChild (0).GetComponent<TextMeshProUGUI> () == null)
         {
            _obj.transform.GetChild (0).gameObject.AddComponent<TextMeshProUGUI> ();
         }
         _obj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text = _section.gradeLevel + " " + _section.name;
         _obj.transform.SetParent (btnSectionContainer.transform);
         currentMaxSection++;
      }

      if (btnSectionContainer.transform.childCount == 0)
      {
         btnEdit.gameObject.SetActive (false);
      }
      else
      {
         if (UserRestrictionController.ins.restriction == 0)
         {
            btnEdit.gameObject.SetActive (true);
         }

      }

      DataService.Close ();
   }

   public void CreateNewSection()
   {
      CreateSectionView view = panelSectionInput.GetComponent<CreateSectionView> ();
      //create section for this device
      if ("".Equals (view.txtSectionName.text))
      {
         MessageBox.ins.ShowOk ("Enter section name.", MessageBox.MsgIcon.msgError, null);
      }
      else
      {

         if (currentMaxSection < maxSectionAllowed)
         {
            bool dup = false;
            for (int i = 0; i < btnSectionContainer.transform.childCount; i++)
            {
               Debug.Log (btnSectionContainer.transform.GetChild (i).gameObject.transform.GetChild (0).gameObject.GetComponent<TextMeshProUGUI> ().text);
               if (view.txtSectionName.text.Equals (btnSectionContainer.transform.GetChild (i)
                  .gameObject.transform.GetChild (0).gameObject.GetComponent<TextMeshProUGUI> ().text))
               {
                  dup = true;
               }

            }
            if (!dup)
            {
               DatabaseSectionController dsc = new DatabaseSectionController ();

               //PlayerPrefs.SetString("activeDatabase", newSection.text + ".db");
               //create the section database
               dsc.CreateSectionDb (view.txtSectionName.text + ".db");
               Debug.Log ("section db file created!");
               //create the section database tables
               dsc.CreateSectionTables (view.txtSectionName.text + ".db");
               Debug.Log ("section db tables created!");

               //insert new section in admin database
               DataService.Open ("system/admin.db");
               AdminSectionsModel asm = new AdminSectionsModel
               {
                  DeviceId = SystemInfo.deviceUniqueIdentifier,
                  SectionId = 1,
                  Description = view.txtSectionName.text,
                  GradeLevel = view.GradeLevel
               };
               DataService._connection.Insert (asm);
               DataService.Close ();
               Debug.Log ("section added into admin sections");

               //create section in section database
               DataService.Open (view.txtSectionName.text + ".db");

               SectionModel model = new SectionModel { DeviceId = SystemInfo.deviceUniqueIdentifier, Description = view.txtSectionName.text };
               DataService._connection.Insert (model);

               GameObject _obj = Instantiate (btnSectionPrefab);
               Section _section = _obj.GetComponent<Section> ();
               SectionModel s = DataService._connection.Table<SectionModel> ().Where (x => x.Description == model.Description).FirstOrDefault ();
               _section.id = s.Id;
               _section.name = view.txtSectionName.text;
               _section.gradeLevel = view.GradeLevel;
               _obj.transform.GetChild (0).GetComponent<TextMeshProUGUI> ().text = _section.name;
               _obj.transform.SetParent (btnSectionContainer.transform);

               panelSectionInput.gameObject.SetActive (false);
               currentMaxSection++;

               DataService.Close ();
               Debug.Log ("section created into section db!");

            }
            else
            {
               MessageBox.ins.ShowOk (view.txtSectionName.text + " already exist.", MessageBox.MsgIcon.msgError, null);
            }

         }
         else
         {
            MessageBox.ins.ShowOk ("Max number of sections allowed already reached.", MessageBox.MsgIcon.msgError, null);
         }
      }
      //PrintSections();
   }

   public void Close()
   {
      gameObject.SetActive (false);
   }

   public void Show()
   {
      gameObject.SetActive (true);
   }

   //Edit-----------------------------
   public void EditSection()
   {
      if (btnSectionContainer.transform.childCount == 0)
      {
         MessageBox.ins.ShowOk ("No section to edit.", MessageBox.MsgIcon.msgInformation,
            null);
      }
      else
      {
         editMode = true;
         MessageBox.ins.ShowOkCancel ("Select section to edit. Click cancel to return.", MessageBox.MsgIcon.msgInformation,
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
      MessageBox.ins.ShowOk ("Edit section cancelled.", MessageBox.MsgIcon.msgInformation, null);
   }

   void EditClose()
   {
      editMode = false;
      EditSectionView view = panelEditSectionInput.GetComponent<EditSectionView> ();
      view.gameObject.SetActive (false);
      view.btnOK.onClick.RemoveAllListeners ();

   }

   public void Edit(Section s)
   {
      EditSectionView view = panelEditSectionInput.GetComponent<EditSectionView> ();
      view.gameObject.SetActive (true);
      view.txtSectionName.text = s.name;

      view.dropdownGradeLevel.value = view.dropdownGradeLevel.options.FindIndex ((i)=> { return i.text.Equals (s.gradeLevel); });


      UpdateSection updateSection = new UpdateSection (view, s);
      view.btnOK.onClick.AddListener (updateSection.UpdateSectionName);
      view.btnClose.onClick.AddListener (EditClose);

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
      if ("".Equals (view.txtSectionName.text))
      {
         MessageBox.ins.ShowOk ("All fields are required.", MessageBox.MsgIcon.msgError, null);
      }

      else if (view.txtSectionName.text.Equals (s.name) && view.GradeLevel.Equals (s.gradeLevel))
      {
         //nothing to update just say updated!
         MessageBox.ins.ShowOk ("Section name updated!", MessageBox.MsgIcon.msgInformation, null);
         SectionController.ins.editMode = false;
         view.gameObject.SetActive (false);
         view.btnOK.onClick.RemoveAllListeners ();
         //view.btnClose.onClick.RemoveAllListeners ();
      }
      else
      {

         //Rename section database
         DatabaseSectionController dsc = new DatabaseSectionController ();
         try
         {
            dsc.RenameDb (s.name + ".db", view.txtSectionName.text + ".db");
         }
         catch (IOException ex)
         {
            Debug.LogError(ex.Message);
            MessageBox.ins.ShowOk ("Please try again.", MessageBox.MsgIcon.msgError, null);
            return;
         }

         //update section in admin database
         DataService.Open ("system/admin.db");
         AdminSectionsModel asm = DataService._connection.Table<AdminSectionsModel> ().Where (x => x.Description == s.name).FirstOrDefault ();
         asm.Description = view.txtSectionName.text;
         asm.GradeLevel = view.GradeLevel;
         DataService._connection.Update (asm);
         DataService.Close ();

         DataService.Open (view.txtSectionName.text + ".db");
         SectionModel model = new SectionModel
         {
            Id = s.id,
            Description = view.txtSectionName.text
         };
         //_connection.Execute ("Update UserTable set currentCar=" + currnetCarNumb + " where
         //ID = "+userID);
         DataService._connection.Execute ("Update SectionModel set Description='" + model.Description + "' where Id='" + model.Id + "'");
         MessageBox.ins.ShowOk ("Section name updated!", MessageBox.MsgIcon.msgInformation, null);
         SectionController.ins.editMode = false;
         SectionController.ins.LoadSectionsSQL ();
         view.gameObject.SetActive (false);
         view.btnOK.onClick.RemoveAllListeners ();

         DataService.Close ();

         //view.btnClose.onClick.RemoveAllListeners ();
      }
   }
}
