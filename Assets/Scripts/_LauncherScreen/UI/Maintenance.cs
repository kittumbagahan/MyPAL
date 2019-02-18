using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AppLauncher
{
    public enum SelectedTab
    {
        Section,
        Student
    }

    public class Maintenance : MonoBehaviour
    {    
        [SerializeField]
        List<GameObject> lstData;

        [SerializeField]
        int maxSectionAllowed,
            currentMaxSection = 0,
            currentMaxStudent,        
            maxStudentAllowed;            

        SelectedTab selectedTab;

        MaintenanceView maintenanceView;

        // Use this for initialization
        void Start()
        {
            DataService.Open("system/admin.db");
            maxSectionAllowed = DataService._connection.Table<NumberOfSectionsModel>().FirstOrDefault().MaxSection;
            DataService.Close();

            // get view
            maintenanceView = GetComponent<MaintenanceView> ();

            // add button events
            maintenanceView.BtnAdd.onClick.AddListener (Add);

            // add toggle events
            // section toggle
            maintenanceView.ToggleSection.onValueChanged.AddListener(SectionToggle);
            // student toggle
            maintenanceView.ToggleStudent.onValueChanged.AddListener(StudentToggle);

            // set toggle section as selected
            maintenanceView.ToggleSection.isOn = true;
        }

        #region METHODS

        public void SectionToggle(bool isOn)
        {
            if (isOn)
            {
                selectedTab = SelectedTab.Section;
                LoadSectionSQL ();
            }
        }

        public void StudentToggle(bool isOn)
        {
            if (isOn)
            {
                selectedTab = SelectedTab.Student;
                LoadStudentSQL ();
            }
        }

        public GameObject CreateData()
        {
            GameObject _data;

            // all is active, instantiate new
            if(lstData.TrueForAll(IsActive))
            {
                _data = Instantiate(maintenanceView.Data, maintenanceView.TransformParentContainer);
                lstData.Add(_data);                
                return _data;
            }
            // Find inactive gameobject
            else
            {
                _data = lstData.Find(IsNotActive);
                _data.SetActive(true);
                return _data;
            }            
        }

        private static bool IsActive(GameObject gameObject)
        {
            return gameObject.activeInHierarchy;
        }

        private static bool IsNotActive(GameObject gameObject)
        {
            return !gameObject.activeInHierarchy;
        }

        void SetToInActive()
        {
            foreach (var gameObject in lstData)
            {
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
        }

        #endregion

        #region SQL

        void Add()
        {
            Debug.Log (string.Format ("Selected Tab is {0}", selectedTab));
            // instantiate panel then add "Add (Selected Tab) class"

            GameObject panel = Instantiate (maintenanceView.SectionPanel, transform);// maintenance panel

            switch (selectedTab)
            {
                case SelectedTab.Section:
                panel.AddComponent<AddSection> ();
                break;   
            }
        }

        void LoadSectionSQL()
        {
            Debug.Log("Load Section");

            SetToInActive();

            DataService.Open("system/admin.db");

            var sections = DataService._connection.Table<AdminSectionsModel>();

            foreach (var section in sections)
            {
                Debug.Log(section.Description);
                Data _data = CreateData().GetComponent<Data>();

                _data.SetData(section.Description);
                _data.AddEvent(() =>
                {
                    // instantiate panel then add "Edit (Selected Tab) class"
                    GameObject panel = Instantiate (maintenanceView.SectionPanel, transform);

                    Debug.Log(string.Format("Description {0}, ID {1}", section.Description, section.Id));
                });

                currentMaxSection++;
            }

            DataService.Close();
        }

        void LoadStudentSQL()
        {
            Debug.Log("Load Student");

            SetToInActive();

            UpdateNumberOfStudents();

            DataService.Open("system/admin.db");
            maxStudentAllowed = DataService._connection.Table<NumberOfStudentsModel>().Where(x => x.Id == 1).FirstOrDefault().MaxStudent;
            DataService.Close();
            DataService.Open();
            //string query = "select * from StudentModel where SectionId='" + StoryBookSaveManager.ins.activeSection_id + "' and Givenname LIKE '" + letter + "%'";
            string query = "select * from StudentModel";
            //var students = DataService._connection.Table<StudentModel> ().Where (x => x.SectionId == StoryBookSaveManager.ins.activeSection_id);
            var students = DataService._connection.Query<StudentModel>(query);
            
            foreach(var student in students)
            {
                string studentName = student.Givenname + " " + student.Middlename + " " + student.Lastname + " " + student.Nickname;
                CreateData().GetComponent<Data>().SetData(studentName);
            }

            DataService.Close();
        }

        void UpdateNumberOfStudents()
        {
            DataService.Open("system/admin.db");
            var sections = DataService._connection.Table<AdminSectionsModel>();
            string[] sectionNames = sections.ToArray().Select(x => x.Description).ToArray();
            DataService.Close();

            currentMaxStudent = 0;
            foreach (string name in sectionNames)
            {
                DataService.Open(name + ".db");
                currentMaxStudent += DataService._connection.Table<StudentModel>().Count();
                DataService.Close();
            }

        }

        #endregion
    }
}
