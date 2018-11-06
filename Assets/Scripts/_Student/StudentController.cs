using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StudentController : MonoBehaviour {
    public static StudentController ins;
    [SerializeField]
    GameObject panelStudentInput;
    [SerializeField]
    GameObject btnStudentContainer;
    [SerializeField]
    GameObject btnStudentPrefab;
    [SerializeField]
    Text txtGivenName;
    [SerializeField]
    Text txtSurname;
    [SerializeField]
    Text txtMiddleName;
    [SerializeField]
    Text txtNickName;
    [SerializeField]
    int currentMaxStudent;
    [SerializeField]
    int maxStudentAllowed;


    void Start()
    {
        if (ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
        }
        if (StoryBookSaveManager.ins.activeUser != "")
        {
            gameObject.SetActive(false);
        }
        if(PlayerPrefs.GetInt("maxNumberOfStudentsAllowed") == 0)
        {
            PlayerPrefs.SetInt("maxNumberOfStudentsAllowed", 10);
        }
        //LoadStudents();
        // for (int i=0; i<10; i++)
        // {
        //     print(PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
        //     "student_id" + i));
        // }

    }
  
    public void LoadStudents()
    {
        maxStudentAllowed = PlayerPrefs.GetInt("maxNumberOfStudentsAllowed");

        int n = 0;
        for(int i=0; i< btnStudentContainer.transform.childCount; i++)
        {
            Destroy(btnStudentContainer.transform.GetChild(i).gameObject);
        }

        for(int i=0; i<maxStudentAllowed; i++)
        {
            if (PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + i) != "")
            {
                GameObject _obj = Instantiate(btnStudentPrefab);
                Student _student = _obj.GetComponent<Student>();
                _student.id = i;
                _student.name = PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
                "student_id" + i);
                _obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _student.name;
                _obj.transform.SetParent(btnStudentContainer.transform);
                n++;
            }
        }
        currentMaxStudent = n;

    }

    public void CreateNewStudent()
    {
        if ("".Equals(txtGivenName.text) || "".Equals(txtMiddleName.text) || "".Equals(txtSurname.text) || "".Equals(txtNickName.text))
        {
            MessageBox.ins.ShowOk("All fields are required.", MessageBox.MsgIcon.msgError, null);
        }
        else
        {
            string studentName = txtGivenName.text + " " + txtMiddleName.text + " " + txtSurname.text + " " + txtNickName.text;
            if (currentMaxStudent < maxStudentAllowed)
            {
                if (!IsDuplicate(studentName))
                {
                    int newId = SetStudentId();
                    PlayerPrefs.SetString("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + newId, studentName);
                    GameObject _obj = Instantiate(btnStudentPrefab);
                    Student _student = _obj.GetComponent<Student>();
                    _student.id = newId;
                    _student.name = studentName;
                    _obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _student.name;
                    _obj.transform.SetParent(btnStudentContainer.transform);

                    panelStudentInput.gameObject.SetActive(false);
                    currentMaxStudent++;
                }
                else
                {
                    MessageBox.ins.ShowOk("Name already exist.", MessageBox.MsgIcon.msgError, null);
                }
            }
            else
            {
                MessageBox.ins.ShowOk("Max number of students allowed already reached.", MessageBox.MsgIcon.msgError, null);
            }
        }
        
        //PrintSections();
    }

    int StudentSelectMax()
    {
        currentMaxStudent = 0;
        int n = 0;
        while (PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + n) != "")
        {
            n++;
        }
        currentMaxStudent = n;
        return n;
    }

    int SetStudentId()
    {
        for (int i=0; i<maxStudentAllowed; i++)
        {
            if(PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + i) == "")
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
        while (PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + n) != "")
        {
            if (PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id +
            "student_id" + n) == s)
            {
                return true;
            }
            n++;
        }
        return false;
    }

    

    public void Show()
    {
        gameObject.SetActive(true);
      
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
