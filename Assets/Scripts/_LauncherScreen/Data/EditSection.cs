using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSection : SectionMaintenanceBase {

    public string ID
    {
        get; set;
    }

    private void Start ()
    {
        SetUp ();
    }

    #region METHODS

    public override void OK ()
    {
        if ("".Equals (sectionView.SectionName))
            sectionView.Sectionrequired ();
        else
        {
            // save data to db
            Debug.Log (string.Format ("EDIT Grade level {0}\nSection name {1}, ID {2}", sectionView.GradeLevel, sectionView.SectionName, ID));
        }
    }

    #endregion
}
