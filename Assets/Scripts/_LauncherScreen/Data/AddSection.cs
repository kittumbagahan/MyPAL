using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSection : MonoBehaviour {

    SectionView sectionView;

	// Use this for initialization
	void Start () {
        sectionView = GetComponent<SectionView>();
        sectionView.OK.onClick.AddListener(OK);
	}

    #region METHODS
    
    void OK()
    {
        if ("".Equals(sectionView.SectionName))
            sectionView.Sectionrequired();
        else
        {
            Debug.Log(string.Format("Grade level {0}\nSection name {1}", sectionView.GradeLevel, sectionView.SectionName));
        }
    }    

    #endregion

}
