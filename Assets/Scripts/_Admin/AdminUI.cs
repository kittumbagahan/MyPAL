using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdminUI : MonoBehaviour {

    string password = "1234";
    [SerializeField]
    GameObject loginCanvas;

    [SerializeField]
    InputField fieldPassword;

    bool isPressed = false;
    float startTime;

	// Use this for initialization
	void Start () {
		
	}
		
    #region EVENTS

    public void Login()
    {
        if(password == fieldPassword.text)
        {
            SceneManager.LoadScene ("Admin");
        }
        else if(fieldPassword.text != "" && fieldPassword.text != password)
        {
            MessageBox.ins.ShowOk("Incorrect password", MessageBox.MsgIcon.msgInformation, null);
        }
    }

    public void Pressed ()
    {
        isPressed = true;
        startTime = Time.time;
        Debug.Log ("pressed");
        StartCoroutine ("_Press");
    }

    public void Released ()
    {
        isPressed = false;
        startTime = 0;
        StopCoroutine ("_Press");
        Debug.Log ("released");
    }

    IEnumerator _Press ()
    {
        Debug.Log ("pressing");
        Debug.Log ("start time " + startTime);

        while (isPressed)
        {
            Debug.Log ("time " + Time.time + ", start time " + startTime + ", total time " + (Time.time - startTime));
            yield return new WaitForSeconds (1);
            if (Time.time - startTime >= 3)
            {
                // exit coroutine, display login canvas
                StopCoroutine ("_Press");
                loginCanvas.SetActive (true);
            }
        }
    }

    #endregion
}
