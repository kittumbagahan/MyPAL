using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionView : MonoBehaviour {

    [SerializeField] InputField sectionName;
    [SerializeField] Dropdown gradeLevel;

    [SerializeField] Button btnOK, btnClose;

    [SerializeField] Text noSectionLabel, title;

    public string SectionName
    {
        get { return sectionName.text; }
    }
    	
    public string GradeLevel
    {
        get { return gradeLevel.captionText.text; }
    }

    public Button OK
    {
        get { return btnOK; }
    }      
    
    public void Sectionrequired()
    {
        noSectionLabel.text = "Section Required!";
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    public void SetTitle(string _title)
    {
        title.text = _title;
    }
}
