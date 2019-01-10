using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using UnityEngine.UI;

using System.IO;
using System.Text;
using System;

using UnityEngine.SceneManagement;

public class GradesCard : MonoBehaviour
{

   AccuracyABC accuracyABC;
   AccuracyAfterTheRain accuracyAfterTheRain;
   AccuracyChatWithCat accuracyChatWithCat;
   AccuracyColorsAllMixedUp accuracyColorsAllMixedUp;
   AccuracyFavoriteBox accuracyFavoriteBox;
   AccuracyJoeyGoesToSchool accuracyJoeyGoesToSchool;
   AccuracySoundsFantastic accuracySoundsFantastic;
   AccuracyTinaAndJun accuracyTinaAndJun;
   AccuracyWhatDidYouSee accuracyWhatDidYouSee;
   AccuracyYummyShapes accuracyYummyShapes;

   [SerializeField]
   List<StudentGradeCard> sgc;
   [SerializeField]
   GameObject txtprefab;
   [SerializeField]
   Transform parent;

   [SerializeField]
   Button btnExportData;
   [SerializeField]
   Text txtData;
   // Use this for initialization
   [SerializeField]
   GameObject loadingPanel;


   // data export
   protected string columns = "Fullname,Word,Observation,Total";
   protected string data;
   protected string textExport;


   IEnumerator IEComputeGrades()
   {
      float progress = 0;
      float counter = 0;
      Text txtLoading = loadingPanel.GetComponentInChildren<Text> ();
      loadingPanel.gameObject.SetActive (true);
      DataService.Open ();

      var students = DataService._connection.Table<StudentModel> ().Where (x => x.SectionId == StoryBookSaveManager.ins.activeSection_id).OrderBy (x => x.Gender);
      txtData.text = "<b>Fullname, Word, Observation, Total</b>\n";

      foreach (var s in students)
      {
         double wordTotalGrade = TotalWordGrade (s.Id);
         double observationTotalGrade = TotalObservationGrade (s.Id);


         if (s.Gender.Equals ("Male")) txtData.text += "<color=#0000a0ff>";
         else txtData.text += "<color=#ff00ffff>";
         if (IsIncomplete ())
         {
            //  txtData.text += "\n" + s.Lastname + " " + s.Givenname + " " + s.Middlename + ", " +
            //"INC" + ", " + "INC" + ", " + "INC";
            txtData.text += "\n" + s.Lastname + " " + s.Givenname + " " + s.Middlename + ", " +
         wordTotalGrade + ", " + observationTotalGrade + ", " + ((wordTotalGrade + observationTotalGrade) / 2).ToString ();
         }
         else
         {
            txtData.text += "\n" + s.Lastname + " " + s.Givenname + " " + s.Middlename + ", " +
           wordTotalGrade + ", " + observationTotalGrade + ", " + ((wordTotalGrade + observationTotalGrade) / 2).ToString ();
         }
         txtData.text += "</color>";

         data += string.Format ("\"{0}, {1} {2}.\"", s.Lastname, s.Givenname, s.Middlename) + "," + wordTotalGrade + "," + observationTotalGrade +
             "," + (wordTotalGrade + observationTotalGrade) + Environment.NewLine;
         counter++;
         progress = (counter / (float) students.Count ()) * 100;
         txtLoading.text = "Loading " + progress.ToString () + "%";
         yield return null;
      }
      loadingPanel.gameObject.SetActive (false);
      DataService.Close ();
   }

   private void OnEnable()
   {
      if (accuracyABC == null)
      {
         sgc = new List<StudentGradeCard> ();
         accuracyABC = new AccuracyABC ();
         accuracyAfterTheRain = new AccuracyAfterTheRain ();
         accuracyChatWithCat = new AccuracyChatWithCat ();
         accuracyColorsAllMixedUp = new AccuracyColorsAllMixedUp ();
         accuracyFavoriteBox = new AccuracyFavoriteBox ();
         accuracyJoeyGoesToSchool = new AccuracyJoeyGoesToSchool ();
         accuracySoundsFantastic = new AccuracySoundsFantastic ();
         accuracyTinaAndJun = new AccuracyTinaAndJun ();
         accuracyWhatDidYouSee = new AccuracyWhatDidYouSee ();
         accuracyYummyShapes = new AccuracyYummyShapes ();
      }

      // data        
      columns += Environment.NewLine + "," + Environment.NewLine;

      StartCoroutine (IEComputeGrades ());

   }

   double TotalWordGrade(int id)
   {
      return (accuracyFavoriteBox.GetAccuracyWord (id) + accuracyABC.GetAccuracyWord (id) + accuracyAfterTheRain.GetAccuracyWord (id) +
          accuracyChatWithCat.GetAccuracyWord (id) + accuracyColorsAllMixedUp.GetAccuracyWord (id) + accuracyJoeyGoesToSchool.GetAccuracyWord (id) +
          accuracySoundsFantastic.GetAccuracyWord (id) + accuracyTinaAndJun.GetAccuracyWord (id) + accuracyWhatDidYouSee.GetAccuracyWord (id) +
          accuracyYummyShapes.GetAccuracyWord (id)) / 10;
   }

   double TotalObservationGrade(int id)
   {
      return (accuracyFavoriteBox.GetAccuracyObservation (id) + accuracyABC.GetAccuracyObservation (id) + accuracyAfterTheRain.GetAccuracyObservation (id) +
          accuracyChatWithCat.GetAccuracyObservation (id) + accuracyColorsAllMixedUp.GetAccuracyObservation (id) + accuracyJoeyGoesToSchool.GetAccuracyObservation (id) +
          accuracySoundsFantastic.GetAccuracyObservation (id) + accuracyTinaAndJun.GetAccuracyObservation (id) + accuracyWhatDidYouSee.GetAccuracyObservation (id) +
          accuracyYummyShapes.GetAccuracyObservation (id)) / 10;
   }

   public override string ToString()
   {
      return string.Format ("{0}", accuracyFavoriteBox.GetAccuracy (1).ToString ());
   }

   bool IsIncomplete()
   {
      if (accuracyFavoriteBox.lstGrade.Contains ("") || accuracyABC.lstGrade.Contains ("") || accuracyAfterTheRain.lstGrade.Contains ("") ||
          accuracyChatWithCat.lstGrade.Contains ("") || accuracyColorsAllMixedUp.lstGrade.Contains ("") || accuracyJoeyGoesToSchool.lstGrade.Contains ("") ||
          accuracySoundsFantastic.lstGrade.Contains ("") || accuracyTinaAndJun.lstGrade.Contains ("") || accuracyWhatDidYouSee.lstGrade.Contains ("") ||
          accuracyYummyShapes.lstGrade.Contains (""))
      {
         return true;
      }
      return false;
   }

   // test
   #region DATA
   public void ExportData()
   {
      SceneManager.LoadScene ("DataImporter");
      //File.WriteAllText(Application.persistentDataPath + "/studentData.csv", textExport);
      //Debug.Log("Check File at " + Application.persistentDataPath);
      //MessageBox.ins.ShowOk("Data export successful!", MessageBox.MsgIcon.msgInformation, null);
   }
   #endregion
}
