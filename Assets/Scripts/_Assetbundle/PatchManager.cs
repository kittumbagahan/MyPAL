using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class PatchManager : CachedAssetBundleLoader {


	/*
     DLC DOWNLOADER
     */
	//[SerializeField]
	//CachedAssetBundleLoader bundleLoader;
	[SerializeField]
	String[] bundle_URL;
	[SerializeField]
	ProgressBar pb;

	//[SerializeField]
	//string isBundle_1_downloaded, isBundle_2_downloaded, isBundle_All_downloaded;
	[SerializeField]
	string productId_1, productId_2, productId_3;
	[SerializeField]
	int retryCount = 0;

	void UnlockAll()
	{
		/*
        PlayerPrefs.SetInt("paid", 1);
        PlayerPrefs.SetInt(productId_1, 1);
        PlayerPrefs.SetInt(productId_2, 1);
        PlayerPrefs.SetInt(productId_3, 1);
        */
	}


	void Start () {
        PlayerPrefs.DeleteAll();
        DataService ds = new DataService ();
      //ds._connection.DeleteAll<BookModel> ();
      //ds._connection.DeleteAll<StudentModel> ();
      //ds._connection.DeleteAll<ActivityModel> ();
      //ds._connection.DeleteAll<SectionModel> ();
      //ds._connection.DeleteAll<StudentActivityModel> ();
      //ds._connection.DeleteAll<StudentBookModel> ();
      //drop tables to update fields on creation if model members have change
      ds._connection.Execute("drop table StudentActivityModel");
      ds._connection.Execute("drop table StudentBookModel");
      ds._connection.Execute("drop table BookModel");
      ds._connection.Execute("drop table StudentModel");
      ds._connection.Execute("drop table ActivityModel");
      ds._connection.Execute("drop table SectionModel");
      ds._connection.Execute ("drop table SubscriptionTimeModel");
      ds._connection.Execute ("drop table ResetPasswordTimesModel");
      ds._connection.Execute ("drop table NumberOfStudentsModel");
      ds._connection.Execute ("drop table AdminPasswordModel");

      //Caching.CleanCache();

      //		PlayerPrefs.SetInt("bundleVersion", 6);
      //		PlayerPrefs.SetInt("paid",1);
      //		PlayerPrefs.SetInt(productId_1,1);
      //		PlayerPrefs.SetInt(productId_2,1);
      //		PlayerPrefs.SetInt(productId_3,1);

      //PlayerPrefs.SetInt("product" + "JoeyGoesToSchool", 0);
      //PlayerPrefs.SetInt("product" + "ChatWithMyCat", 0);
      //PlayerPrefs.SetInt("product" + "SoundsFantastic", 0);
      //PlayerPrefs.SetInt("product" + "TinaAndJun", 0);
      //PlayerPrefs.SetInt("product" + "WhatDidYouSee", 0);
      //UnlockAll();

      pb.gameObject.SetActive(false);
		//if (PlayerPrefs.GetInt("paid") == 1)
		//{
		//	if (Application.internetReachability == NetworkReachability.NotReachable)
		//	{
		//		print("not internet");
		//		if(PlayerPrefs.GetInt(productId_1) == 1 && PlayerPrefs.GetInt(productId_1 + "downloaded") == 0)
		//		{
		//			MessageBox.ins.ShowOk("Cannot Download DLC. Internet connection is required.", MessageBox.MsgIcon.msgInformation, new UnityAction(ShowShelf));
		//		}

		//		else if(PlayerPrefs.GetInt(productId_2) == 1 && PlayerPrefs.GetInt(productId_2 + "downloaded") == 0)
		//		{
		//			MessageBox.ins.ShowOk("Cannot Download DLC. Internet connection is required.", MessageBox.MsgIcon.msgInformation, new UnityAction(ShowShelf));
		//		}

		//		else if(PlayerPrefs.GetInt(productId_3) == 1 && PlayerPrefs.GetInt(productId_3 + "downloaded") == 0)
		//		{
		//			MessageBox.ins.ShowOk("Unable to download In App Purchases. Internet connection is required.", MessageBox.MsgIcon.msgInformation, new UnityAction(ShowShelf));
		//		}
		//		else{
		//			ShowShelf();
		//		}

		//	}
		//	else
		//	{

		//		try
		//		{
		//			// StartCoroutine(IEDownload_All());
		//			//CheckDLC();

		//			StartCoroutine(IECheckForUpdates());
		//		}
		//		catch (Exception ex)
		//		{
		//			print(ex);
		//		}

		//		OnDownload += pb.SetProgress;
		//	}

		//	//OnFinished += ShowShelf;
		//}
		//else
		//{
		//	ShowShelf();
		//}

	}


	IEnumerator IECheckForUpdates()
	{
		CheckBundleVersion();
		Tammytam.ins.Say2("Checking \nfor updates.");
		yield return new WaitForSeconds(2f);
		CheckDLC();
	}

	void CheckDLC()
	{
		if (PlayerPrefs.GetInt(productId_3) == 0)
		{
			if(PlayerPrefs.GetInt(productId_2) == 1)
			{
				if(PlayerPrefs.GetInt(productId_2 + "downloaded") == 0)
				{
					Tammytam.ins.Say2("We are \ndownloading \nyour books\nnow.");
					pb.gameObject.SetActive(true);
					StartCoroutine(IEDownload_All());
					print("Download bundle 2");
				}else{
					Tammytam.ins.Say2("Enjoy your books!");
					SceneManager.LoadSceneAsync("Bookshelf");
				}
			}
			else if(PlayerPrefs.GetInt(productId_1) == 1 && PlayerPrefs.GetInt(productId_2) == 0)
			{
				if(PlayerPrefs.GetInt(productId_1 + "downloaded") == 0)
				{
					Tammytam.ins.Say2("We are \ndownloading \nyour books\nnow.");
					pb.gameObject.SetActive(true);
					StartCoroutine(IEDownloadBundle_1());
					print("Download bundle 1");
				}
				else
				{
					Tammytam.ins.Say2("Enjoy your books!");
					SceneManager.LoadSceneAsync("Bookshelf");
				}
			}

		}
		else
		{
			if(PlayerPrefs.GetInt(productId_3 + "downloaded") == 0)
			{
				Tammytam.ins.Say2("We are \ndownloading \nyour books\nnow.");
				pb.gameObject.SetActive(true);
				StartCoroutine(IEDownload_All());
				print("Downoad all");	
			}
			else
			{

				Tammytam.ins.Say2("Enjoy your books!");
				SceneManager.LoadSceneAsync("Bookshelf");
			}
		}
	}

	void CheckBundleVersion()
	{
		if(PlayerPrefs.GetInt("bundleVersion") != version)
		{
			Caching.ClearCache();
			PlayerPrefs.SetInt("bundleVersion", version);
			PlayerPrefs.SetInt(productId_3 + "downloaded", 0);
			PlayerPrefs.SetInt(productId_2 + "downloaded", 0);
			PlayerPrefs.SetInt(productId_1 + "downloaded", 0);
		}
	}
	IEnumerator IEDownload_All()
	{
		//yield return Caching.CleanCache();

		for (int i = 0; i < bundle_URL.Length; i++ )
		{
			pb.TextTitle.text = "Downloading your books " + " " + i + "/" + bundle_URL.Length;
			yield return StartCoroutine(IEGetFromCache(bundle_URL[i]));

		}
		if (success)
		{
			PlayerPrefs.SetInt("product" + "ABC_Circus", 1);
			PlayerPrefs.SetInt("product" + "AfterTheRain", 1);
			PlayerPrefs.SetInt("product" + "Colors All Mixed Up", 1);
			PlayerPrefs.SetInt("product" + "YummyShapes", 1);

			PlayerPrefs.SetInt("product" + "JoeyGoesToSchool", 1);
			PlayerPrefs.SetInt("product" + "ChatWithMyCat", 1);
			PlayerPrefs.SetInt("product" + "SoundsFantastic", 1);
			PlayerPrefs.SetInt("product" + "TinaAndJun", 1);
			PlayerPrefs.SetInt("product" + "WhatDidYouSee", 1);

			PlayerPrefs.SetInt(productId_3 + "downloaded", 1); //FOR CHECKING DLC
			PlayerPrefs.SetInt(productId_2 + "downloaded", 1); //if bundle_1 bought first then bundle_2 use this method
			pb.TextTitle.text = "Download Complete";
			OnDownload -= pb.SetProgress;
			SceneManager.LoadSceneAsync("Bookshelf");

		}
		else
		{
			if (retryCount < 10)
			{
				//MessageBox.ins.ShowOk("INTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, null);
				pb.TextTitle.text = "Connection error... Retrying download...";
				yield return new WaitForSeconds(2f);
				retryCount++;
				StartCoroutine(IEDownload_All());
			}
			else
			{
				MessageBox.ins.ShowOk("INTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, new UnityAction(ConnectionErrMsg_2));
			}
		}

	}
	/*
     \ abc
     * after the rain
     * colors
     * yummyshapes
     */

	/*
	sounds
	* tina
	* joey
	* what
	* chatwith my cat
	*/
	IEnumerator IEDownloadBundle_1(){

		for (int i = 0; i < 4; i++ )
		{
			pb.TextTitle.text = "Downloading your books " + " " + i + "/4";
			yield return StartCoroutine(IEGetFromCache(bundle_URL[i]));

		}
		if (success)
		{
			//unlock the books in the carousel
			PlayerPrefs.SetInt("product" + "ABC_Circus", 1);
			PlayerPrefs.SetInt("product" + "AfterTheRain", 1);
			PlayerPrefs.SetInt("product" + "Colors All Mixed Up", 1);
			PlayerPrefs.SetInt("product" + "YummyShapes", 1);

			PlayerPrefs.SetInt(productId_1 + "downloaded", 1); //FOR CHECKING DLC

			pb.TextTitle.text = "Download Complete";
			OnDownload -= pb.SetProgress;
			SceneManager.LoadSceneAsync("Bookshelf");
		}
		else
		{
			if (retryCount < 10)
			{
				//MessageBox.ins.ShowOk("INTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, null);
				pb.TextTitle.text = "Connection error... Retrying download...";
				yield return new WaitForSeconds(2f);
				retryCount++;
				StartCoroutine(IEDownloadBundle_1());
			}
			else
			{
				MessageBox.ins.ShowOk("INTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, new UnityAction(ConnectionErrMsg_0));
			}
		}
	}

	IEnumerator IEDownloadBundle_2()
	{

		for (int i = 4; i < bundle_URL.Length; i++)
		{
			pb.TextTitle.text = "Downloading your books " + " " + i + "/" + bundle_URL.Length;
			yield return StartCoroutine(IEGetFromCache(bundle_URL[i]));

		}
		if (success)
		{
			//unlock the books in the carousel
			PlayerPrefs.SetInt("product" + "JoeyGoesToSchool", 1);
			PlayerPrefs.SetInt("product" + "ChatWithMyCat", 1);
			PlayerPrefs.SetInt("product" + "SoundsFantastic", 1);
			PlayerPrefs.SetInt("product" + "TinaAndJun", 1);
			PlayerPrefs.SetInt("product" + "WhatDidYouSee", 1);

			PlayerPrefs.SetInt(productId_2 + "downloaded", 1); //FOR CHECKING DLC

			pb.TextTitle.text = "Download Complete";
			OnDownload -= pb.SetProgress;
			SceneManager.LoadSceneAsync("Bookshelf");
		}
		else
		{
			if (retryCount < 10)
			{
				//MessageBox.ins.ShowOk("INTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, null);
				pb.TextTitle.text = "Connection error... Retrying download...";
				yield return new WaitForSeconds(2f);
				retryCount++;
				StartCoroutine(IEDownloadBundle_2());
			}
			else
			{
				MessageBox.ins.ShowOk("INTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, new UnityAction(ConnectionErrMsg_1));
			}
		}
	}

	void ConnectionErrMsg_0()
	{
		MessageBox.ins.ShowQuestion("Retry download?", MessageBox.MsgIcon.msgInformation, new UnityAction(RetryDownload_1), new UnityAction(BeforeCloseMsg));
	}
	void ConnectionErrMsg_1()
	{
		MessageBox.ins.ShowQuestion("Retry download?", MessageBox.MsgIcon.msgInformation, new UnityAction(RetryDownload_2), new UnityAction(BeforeCloseMsg));
	}
	void ConnectionErrMsg_2()
	{
		MessageBox.ins.ShowQuestion("Retry download?", MessageBox.MsgIcon.msgInformation, new UnityAction(RetryDownload_All), new UnityAction(BeforeCloseMsg));
	}
	void RetryDownload_1()
	{
		retryCount = 0;
		StartCoroutine(IEDownloadBundle_1());
	}

	void RetryDownload_2()
	{
		retryCount = 0;
		StartCoroutine(IEDownloadBundle_2());
	}
	void RetryDownload_All()
	{
		retryCount = 0;
		StartCoroutine(IEDownload_All());
	}
	void BeforeCloseMsg()
	{
		MessageBox.ins.ShowOk("Try downloading your books later.", MessageBox.MsgIcon.msgInformation, new UnityAction(CloseApp));
	}
	void CloseApp()
	{
		Application.Quit();
	}

	void ShowShelf()
	{
		//Application.LoadLevelAsync("Bookshelf");
		SceneManager.LoadSceneAsync("Bookshelf");
		OnDownload -= pb.SetProgress;
		OnFinished -= ShowShelf;

	}
}
