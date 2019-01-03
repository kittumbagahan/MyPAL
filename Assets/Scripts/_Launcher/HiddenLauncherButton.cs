using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HiddenLauncherButton : MonoBehaviour, IPointerClickHandler
{

    int clicks = 0;

    bool clickActive = false;

    void ResetClicks()
    {
        clicks = 0;
        clickActive = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!clickActive)
        {
            clickActive = true;
            Invoke("ResetClicks", 3);
        }
        clicks++;
        print("hidden clicks " + clicks.ToString());
        if (clicks >= 5)
        {
            SceneManager.LoadScene("Launcher");
        }
    }
}
