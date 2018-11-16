using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateDBScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start () {
		StartSync();
	}

    private void StartSync()
    {
        var ds = new DataService("tempDatabase.db");
        ds.CreateDB();
      var books = ds.GetBooks ();
      var acts = ds.GetActivities ();
      //ToConsole (books);
      ToConsole (acts);
        //var people = ds.GetPersons ();
        //ToConsole (people);
        //people = ds.GetPersonsNamedRoberto ();
        //ToConsole("Searching for Roberto ...");
        //ToConsole (people); 
    }
	
	private void ToConsole(IEnumerable<BookModel> people){
		foreach (var person in people) {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString());
		}
	}
   private void ToConsole(IEnumerable<ActivityModel> people)
   {
      foreach (var person in people)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}
}
