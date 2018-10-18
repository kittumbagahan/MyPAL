using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class ProgressBar : MonoBehaviour {

    private RectTransform foreProgress, backgroundProgess;
    [SerializeField]
    private TextMeshProUGUI txtProgress, txtTitle;
    [SerializeField]
    float bg_Max_Width;
    float fore_width;
    public TextMeshProUGUI TextTitle {
        set { txtTitle = value; }
        get { return txtTitle; }
    }
    void Start()
    {
        foreProgress = GameObject.Find("fore").GetComponent<RectTransform>();
        backgroundProgess = GameObject.Find("bg").GetComponent<RectTransform>();
        //txtProgress = GameObject.Find("downloadProgress").GetComponent<TextMeshProUGUI>();
       // txtTitle = GameObject.Find("title").GetComponent<TextMeshProUGUI>();


        bg_Max_Width = backgroundProgess.GetWidth();
        //StartCoroutine(test());
    }

	public TextMeshProUGUI TxtTitle{
		set{txtTitle = value;}
		get{return txtTitle;}
	}

	public void SetTitle(string s)
	{
		txtTitle.text =s;
	}

    public void SetProgress(float downloadProgress)
    {
        if(downloadProgress * 100 == 0)
        {
            txtProgress.text = "Loading...";
        }else
        {
            txtProgress.text = (downloadProgress * 100).ToString("0.00") + "%";
            //if(foreProgress.GetWidth() < bg_Max_Width)
            //print(downloadProgress + "%");
                
        }
        foreProgress.SetWidth(bg_Max_Width * downloadProgress);
        
    }

	// number of process convert to 100%
	// n process = 2300
	//800 * n%


    //800 = 100%
    //
    void Update()
    { 
        
    }

   

}
