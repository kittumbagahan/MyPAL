using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqlLoadingPanel : MonoBehaviour {

    public static SqlLoadingPanel ins;
    private void Awake()
    {
        ins = this;
        gameObject.SetActive(false);
    }
    void Start () {
       
	}
	
	public void Show(float duration)
    {
        gameObject.SetActive(true);
        Invoke("Hide",duration);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
