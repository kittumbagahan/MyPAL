using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionMaintenanceBase : MonoBehaviour {

    protected SectionView sectionView;

    #region METHODS

    protected void SetUp()
    {
        sectionView = GetComponent<SectionView> ();
        if (sectionView != null)
        {
            sectionView.OKButton.onClick.AddListener (OK);
            sectionView.CloseButton.onClick.AddListener (sectionView.Close);
        }
    }

    public virtual void OK ()
    {
        if ("".Equals (sectionView.SectionName))
            sectionView.Sectionrequired ();
        else
        {
            Debug.Log (string.Format ("Grade level {0}\nSection name {1}", sectionView.GradeLevel, sectionView.SectionName));
        }
    }

    #endregion
}
