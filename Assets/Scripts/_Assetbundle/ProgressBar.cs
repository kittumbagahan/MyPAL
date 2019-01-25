using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class ProgressBar : MonoBehaviour {

    [SerializeField]
    private RectTransform foreProgress, backgroundProgess;
    [SerializeField]
    private Text txtProgress, txtTitle;
    [SerializeField]
    float bg_Max_Width;
    float fore_width;
    public Text TextTitle {
        set { txtTitle = value; }
        get { return txtTitle; }
    }
    void Start()
    {

        bg_Max_Width = backgroundProgess.GetWidth();
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
            foreProgress.SetWidth(bg_Max_Width * downloadProgress);
        }
        else
        {
            if (downloadProgress <= 1)
            {
                txtProgress.text = (downloadProgress * 100).ToString("0.00") + "%";
                foreProgress.SetWidth(bg_Max_Width * downloadProgress);
            }

        }
       
       
        
    }

	// number of process convert to 100%
	// n process = 2300
	//800 * n%


    //800 = 100%
    //
   

   

}
