using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using _Assetbundle;

public class SceneLoader : MonoBehaviour
{


   [SerializeField]
   private string sceneToload;
   [SerializeField]
   GameObject loading;
   [SerializeField]
   bool isAssetBundle;
   [SerializeField]
   string assetBunldeUrlKey; //Beware of this. why? You'll find out soon.
   [SerializeField]
   string assetBundleUrlVersionKey;

   public bool IsAssetBundle
   {
      set
      {
         isAssetBundle = value;
      }
   }


   public string UrlKey
   {
      set
      {
         assetBunldeUrlKey = value;
      }
   }

   public string VersionKey
   {
      set
      {
         assetBundleUrlVersionKey = value;
      }
   }

   public string SceneToLoad
   {
      set { sceneToload = value; }
      get { return sceneToload; }
   }

   void Start()
   {
      if (isAssetBundle)
      {
         if (PlayerPrefs.GetString (assetBunldeUrlKey).Equals (""))
         {
            isAssetBundle = false;
         }
      }
   }

   public void AsyncLoadStr(string name)
   {
      if (loading != null) loading.gameObject.SetActive (true);

      if (isAssetBundle)
      {

         AssetBundleInfo.ActivityScene.urlKey = assetBunldeUrlKey;
         AssetBundleInfo.ActivityScene.versionKey = assetBundleUrlVersionKey;
         AssetBundleInfo.ActivityScene.isAssetBundle = isAssetBundle;
         AssetBundleInfo.ActivityScene.name = name;

         try
         {

            string url = PlayerPrefs.GetString (assetBunldeUrlKey);
            int version = PlayerPrefs.GetInt (assetBundleUrlVersionKey);

            EmptySceneLoader.ins.loadUrl = url;
            EmptySceneLoader.ins.loadVersion = version;
            EmptySceneLoader.ins.sceneToLoad = sceneToload;
            EmptySceneLoader.ins.isAssetBundle = true;

            //this is from book so get reference from AssetBundleInfo where it has the recent loaded scene
            EmptySceneLoader.ins.unloadUrl = PlayerPrefs.GetString (AssetBundleInfo.BookScene.urlKey);
            EmptySceneLoader.ins.unloadVersion = PlayerPrefs.GetInt (AssetBundleInfo.BookScene.versionKey);
            EmptySceneLoader.ins.unloadAll = false;

            SceneManager.LoadSceneAsync ("empty");

            //LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(PlayerPrefs.GetString(assetBunldeUrlKey), PlayerPrefs.GetInt(assetBundleUrlVersionKey));
            //loader.OnLoadSceneFail += Fail;
            //loader.OnLoadSceneSuccess += Success;
            //StartCoroutine(loader.IEStreamAssetBundle());
         }
         catch (LoadSceneFromAssetBundleException ex)
         {
            Debug.LogError ("The book url key downloaded from assetbundle not found.\n Download try downloading the book again from the launcher.");
         }


      }
      else
      {         
         if ("".Equals (name))
         {
            name = sceneToload;
         }
         EmptySceneLoader.ins.isAssetBundle = false;
         AssetBundleInfo.ActivityScene.isAssetBundle = false;
         EmptySceneLoader.ins.sceneToLoad = name;
         //SceneManager.LoadSceneAsync(name);
         SceneManager.LoadSceneAsync ("empty");
         
         if(name == "BookShelf")
            StudentOnline();
      }      
   }

   private static void StudentOnline()
   {
      DataService.Open();
      var studentModel = DataService.StudentModel(StoryBookSaveManager.ins.activeUser_id);
      DataService.Close();
      MainNetwork.Instance.StudentOnline(studentModel);
   }

   void Fail()
   {
      Debug.Log ("Loading scene fail.. Loading default");
      SceneManager.LoadSceneAsync (sceneToload);
   }

   void Success()
   {
      Debug.Log ("Loading scene success");
   }

   void OnDestroy()
   {
      Item.RemoveSubscribers ();
      ObjectToSpot.RemoveSubscribers ();
   }
}
