using UnityEngine;
using System.Collections;

public class SubtitleManager : MonoBehaviour {

    public StoryBookPlayer[] subtitle;
    [SerializeField]
    int recentSubs = -1;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowSubs(int index)
    {
        //print(index + " waaaaaaaaaaaaaaaaaaaaaaa");
        HideRecent();
        recentSubs = index;
        //StoryBookPlayer player = null;
        subtitle[index].gameObject.SetActive(true);
        //player = subtitle[index].GetComponent<StoryBookPlayer>();
        //player.PlayTextAnimation();
        subtitle[index].PlayTextAnimation();
    }

    void HideRecent()
    {
        if (recentSubs != -1)
        {
            subtitle[recentSubs].gameObject.SetActive(false);
        }
        
    }
}
