using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSection : SectionMaintenanceBase {

    private void Start ()
    {
        SetUp ();
    }

    #region METHODS

    public override void OK()
    {
        if ("".Equals(sectionView.SectionName))
            sectionView.Sectionrequired();
        else
        {
            // save data to db
           Debug.Log(string.Format("ADD Grade level {0}\nSection name {1}", sectionView.GradeLevel, sectionView.SectionName));
        }
    }    

    #endregion

}
