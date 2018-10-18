using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnableGames : MonoBehaviour {

    public static EnableGames ins;

    void Start()
    {
        ins = this;

        if (PlayerPrefs.GetInt("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id +
            StoryBookSaveManager.ins.selectedBook + "have_read") == 0)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }
    }

    public void Enable()
    {
        if(PlayerPrefs.GetInt("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id + 
            StoryBookSaveManager.ins.selectedBook + "have_read") == 0)
        {
            PlayerPrefs.SetInt("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id +
                StoryBookSaveManager.ins.selectedBook + "have_read", 1);
            this.GetComponent<Button>().interactable = true;
        }
          
    }
}
