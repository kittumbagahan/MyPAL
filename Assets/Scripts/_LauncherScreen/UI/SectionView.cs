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

    public Dropdown DropDownGradeLevel
    {
        get { return gradeLevel; }
    }

    public Button OKButton
    {
        get { return btnOK; }
    }      

    public Button CloseButton
    {
        get { return btnClose; }
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
