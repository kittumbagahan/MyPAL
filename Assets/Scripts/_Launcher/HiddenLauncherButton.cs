using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HiddenLauncherButton : MonoBehaviour, IPointerClickHandler
{

    int clicks = 0;

    bool clickActive = false;

   [SerializeField]
   GameObject pnlLoading;

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
         LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle (PlayerPrefs.GetString ("Launcher_url_key"), PlayerPrefs.GetInt("Launcher_version_key"));
         loader.OnLoadSceneFail += FailLoad;
         loader.OnLoadSceneSuccess += SuccessLoad;
         StartCoroutine (loader.IEStreamAssetBundle ());
         //SceneManager.LoadScene("Launcher");
        }
    }

   void FailLoad()
   {
      pnlLoading.SetActive (true);
      Debug.Log ("Launcher loaded default");
      SceneManager.LoadSceneAsync ("Launcher");
   }
   void SuccessLoad()
   {
      Debug.Log ("Launcher loaded assetbundle");
      pnlLoading.SetActive (true);

   }
}
