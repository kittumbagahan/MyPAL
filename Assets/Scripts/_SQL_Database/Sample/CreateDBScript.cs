using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class CreateDBScript : MonoBehaviour
{
    [SerializeField]
    TimeUsageCounter timeUsageCounter;
    public Text DebugText;

    // Use this for initialization
    void Start()
    {

        //timeUsageCounter.Init(); //NOT A CREATE
        //PlayerPrefs.SetInt("subscriptionTime_table", 0);
        //PlayerPrefs.SetInt("adminDatabaseCreate", 0);

        //THE time here is a problem, the file creation is time/availability time is random
       
        if (0.Equals(PlayerPrefs.GetInt("subscriptionTime_table")))
        {
            SqlLoadingPanel.ins.Show(22);
            StartCoroutine(IECreate(new UnityAction[] {
                () =>
                {
                    if (PlayerPrefs.GetInt("adminDatabaseCreate").Equals(0))
                    {
                        DatabaseAdminController dac = new DatabaseAdminController();
                        StartCoroutine(dac.IECreate());
                        PlayerPrefs.SetInt("adminDatabaseCreate", 1);
                        
                    }
                },
                () =>
                {
                    if (0.Equals(PlayerPrefs.GetInt("subscriptionTime_table")))
                    {
                        DatabaseController dc = new DatabaseController();
                        dc.CreateSystemDB("subscription.db");
                        Debug.Log("subs created");
                        StartCoroutine(IECreate(new UnityAction[]{
                                () =>
                                {
                                     Debug.Log("subs opened");
                                    DataService.Open("system/subscription.db");

                                    DataService._connection.CreateTable<SubscriptionTimeModel>();
                                    SubscriptionTimeModel model = new SubscriptionTimeModel
                                    {
                                        SettedTime = 1080000, //300hrs to seconds
                                        Timer = 1080000
                                    };
                                    DataService._connection.Insert(model);
                                    var subs = DataService.GetSubscription();
                                    ToConsole(subs);

                                    DataService.Close();
                                    PlayerPrefs.SetInt("subscriptionTime_table", 1);
                                }
                            },
                            new float[]{5}));


                    }
                },
                ()=>{
                      Debug.Log("subs closed");
                    timeUsageCounter.Init();
                }
            },
           new float[] { 1, 10, 11 }));
        }
        else
        {
            timeUsageCounter.Init();
        }






    }

    IEnumerator IECreate(UnityAction[] actions, float[] time)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            yield return new WaitForSeconds(time[i]);
            if (actions[i] != null)
            {
                Debug.Log(time[i]);
                actions[i]();

            }

        }
    }

    private void ToConsole(IEnumerable<SubscriptionTimeModel> model)
    {
        foreach (var person in model)
        {
            Debug.Log(person.ToString());
        }
    }

    private void ToConsole(string msg)
    {
        DebugText.text += System.Environment.NewLine + msg;
        Debug.Log(msg);
    }
}
