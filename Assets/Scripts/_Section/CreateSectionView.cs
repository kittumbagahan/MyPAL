using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateSectionView : MonoBehaviour {

   public InputField txtSectionName;
   [SerializeField]
   Dropdown dropdownGradeLevel;

   public Button btnOK;
   public Button btnClose;

   public string GradeLevel{
      get {
         return dropdownGradeLevel.captionText.text;
      }
      private set { }
   }
}
