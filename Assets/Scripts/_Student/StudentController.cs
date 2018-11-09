using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class StudentController : MonoBehaviour {
    public static StudentController ins;
    [SerializeField]
    GameObject panelCreateStudentInput;
    [SerializeField]
    GameObject panelEditStudentInput;
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

    public bool editMode = false;

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

                    panelCreateStudentInput.gameObject.SetActive(false);
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

    //EDIT ------------------------------------
    public void EditStudent()
    {
        editMode = true;
        MessageBox.ins.ShowOkCancel("Select student to edit. Click cancel to return.", MessageBox.MsgIcon.msgInformation,
            EditYes, EditCancel);
    }

    void EditYes()
    {
        editMode = true;
       
    }
    void EditCancel()
    {
        editMode = false;
        MessageBox.ins.ShowOk("Edit student cancelled.", MessageBox.MsgIcon.msgInformation, null);
    }
    
   void EditClose()
   {
      editMode = false;
      EditStudentView view = panelEditStudentInput.GetComponent<EditStudentView> ();
      view.btnOK.onClick.RemoveAllListeners ();
   }
   
    public void Edit(Student student)
    {
        EditStudentView view = panelEditStudentInput.GetComponent<EditStudentView>();
        view.gameObject.SetActive(true);
        view.txtGivenName.text = student.name.Split(' ')[0];
        view.txtMiddleName.text = student.name.Split(' ')[1];
        view.txtSurname.text = student.name.Split(' ')[2];
        view.txtNickname.text = student.name.Split(' ')[3];

        

        UpdateStudent updateStudent = new UpdateStudent(view, student);
        print("checking update " + student.name);
        view.btnOK.onClick.AddListener(updateStudent.UpdateStudentName);
        view.btnClose.onClick.AddListener (EditClose);
    }

   
}

class UpdateStudent
{

    EditStudentView view;
    Student s;
    public UpdateStudent(EditStudentView view, Student s)
    {
        this.view = view;
        this.s = s;
    }
    public void UpdateStudentName()
    {
        if ("".Equals(view.txtGivenName.text) || "".Equals(view.txtMiddleName.text) || "".Equals(view.txtSurname.text) || "".Equals(view.txtNickname.text))
        {
            MessageBox.ins.ShowOk("All fields are required.", MessageBox.MsgIcon.msgError, null);
        }

        else if (view.txtGivenName.text.Equals(s.name.Split(' ')[0]) && view.txtMiddleName.text.Equals(s.name.Split(' ')[1]) && view.txtSurname.text.Equals(s.name.Split(' ')[2])
            && view.txtNickname.text.Equals(s.name.Split(' ')[3]))
        {
            //nothing to update just say updated!
            MessageBox.ins.ShowOk("Student name updated!", MessageBox.MsgIcon.msgInformation, null);
            StudentController.ins.editMode = false;
            view.btnOK.onClick.RemoveAllListeners ();
      }
        else
        {
            PlayerPrefs.SetString("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + s.id,
            view.txtGivenName.text + " " + view.txtMiddleName.text + " " + view.txtSurname.text + " " + view.txtNickname.text);
            StudentController.ins.LoadStudents();
            MessageBox.ins.ShowOk("Student name updated!", MessageBox.MsgIcon.msgInformation, null);
            StudentController.ins.editMode = false;
            view.btnOK.onClick.RemoveAllListeners ();
      }
    }
}
