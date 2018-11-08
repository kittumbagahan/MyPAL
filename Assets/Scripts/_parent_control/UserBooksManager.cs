using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class UserBooksManager : MonoBehaviour {

    

    public Sprite sprtFavBox,
        sprtChatCat,
        sprtColorsMixed,
        sprtAfterTheRain,
        sprtJoeySchool,
        sprtSoundsFan,
        sprtYumShapes,
        sprtTinaJun,
        sprtWhatSee,
        sprtAbc;
    public List<UserBook> userBookLst;
    public List<int> tempLst;


    [SerializeField]
    TextMeshProUGUI txtUserBookHeader;
    [SerializeField]
    Text utxtBookHeader;
    
    public string userId;

    [SerializeField]
    GameObject utoBubble;
	void Start () {
        print("WOW");
        utoBubble.SetActive(false);
	}

    public void Show(string key)
    {
        gameObject.SetActive(true);
        //get oldUsername name by slotID(s)
        print("key is " + key);
        userId = key; // PlayerPrefs.GetString(key);
        //txtUserBookHeader.text = username + "'s" + " favorite books";
        utxtBookHeader.text = PlayerPrefs.GetString(key).Split(' ')[3] + "'s" + " favorite books";

        LoadUsage();

        Invoke("Sort",1f);
    }


     void LoadUsage()
    {
        //hindi nakaayos sa image                                                                  
      
        //StoryBookSaveManager.instance.oldUsername + "FavoriteBox";
// 
//        AfterTheRain
//          ABC_Circus
//          ChatWithMyCat
//          Colors All Mixed Up
//          JoeyGoesToSchool
//          SoundsFantastic
//          TinaAndJun
//          WhatDidYouSee
//          YummyShapes
        for (int i = 0; i < userBookLst.Count; i++ )
        {
            userBookLst[i].readCount = 0;
            userBookLst[i].playedCount = 0;
        }

        userBookLst[0].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.FAVORITE_BOX);
        userBookLst[0].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.FAVORITE_BOX);
        userBookLst[0].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.FAVORITE_BOX);
        // userBookLst[0].gameObject.AddComponent<AccuracyFavoriteBox>();
        // userBookLst[0].bookAccuracy = userBookLst[0].GetComponent<BookAccuracy>();

        userBookLst[1].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.AFTER_THE_RAIN);
        userBookLst[1].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.AFTER_THE_RAIN);
        userBookLst[1].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.AFTER_THE_RAIN);
        // userBookLst[1].gameObject.AddComponent<AccuracyAfterTheRain>();
        // userBookLst[1].bookAccuracy = userBookLst[1].GetComponent<BookAccuracy>();

        userBookLst[2].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.ABC_CIRCUS);
        userBookLst[2].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.ABC_CIRCUS);
        userBookLst[2].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.ABC_CIRCUS);
        // userBookLst[2].gameObject.AddComponent<AccuracyABC>();
        // userBookLst[2].bookAccuracy = userBookLst[2].GetComponent<BookAccuracy>();

        userBookLst[3].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.CHAT_WITH_MY_CAT);
        userBookLst[3].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.CHAT_WITH_MY_CAT);
        userBookLst[3].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.CHAT_WITH_MY_CAT);
        // userBookLst[3].gameObject.AddComponent<AccuracyChatWithCat>();
        // userBookLst[3].bookAccuracy = userBookLst[3].GetComponent<BookAccuracy>();

        userBookLst[4].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.COLORS_ALL_MIXED_UP);
        userBookLst[4].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.COLORS_ALL_MIXED_UP);
        userBookLst[4].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.COLORS_ALL_MIXED_UP);
        // userBookLst[4].gameObject.AddComponent<AccuracyColorsAllMixedUp>();
        // userBookLst[4].bookAccuracy = userBookLst[4].GetComponent<BookAccuracy>();

        userBookLst[5].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.JOEY_GO_TO_SCHOOL);
        userBookLst[5].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.JOEY_GO_TO_SCHOOL);
        userBookLst[5].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.JOEY_GO_TO_SCHOOL);
        // userBookLst[5].gameObject.AddComponent<AccuracyJoeyGoesToSchool>();
        // userBookLst[5].bookAccuracy = userBookLst[5].GetComponent<BookAccuracy>();

        userBookLst[6].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.SOUNDS_FANTASTIC);
        userBookLst[6].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.SOUNDS_FANTASTIC);
        userBookLst[6].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.SOUNDS_FANTASTIC);
        // userBookLst[6].gameObject.AddComponent<AccuracySoundsFantastic>();
        // userBookLst[6].bookAccuracy = userBookLst[6].GetComponent<BookAccuracy>();

        userBookLst[7].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.TINA_AND_JUN);
        userBookLst[7].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.TINA_AND_JUN);
        userBookLst[7].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.TINA_AND_JUN);
        // userBookLst[7].gameObject.AddComponent<AccuracyTinaAndJun>();
        // userBookLst[7].bookAccuracy = userBookLst[7].GetComponent<BookAccuracy>();

        userBookLst[8].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.WHAT_DID_YOU_SEE);
        userBookLst[8].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.WHAT_DID_YOU_SEE);
        userBookLst[8].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.WHAT_DID_YOU_SEE);
        // userBookLst[8].gameObject.AddComponent<AccuracyWhatDidYouSee>();
        // userBookLst[8].bookAccuracy = userBookLst[8].GetComponent<BookAccuracy>();

        userBookLst[9].readCount += PlayerPrefs.GetInt("read" + userId + StoryBook.YUMMY_SHAPES);
        userBookLst[9].readCount += PlayerPrefs.GetInt("readItToMe" + userId + StoryBook.YUMMY_SHAPES);
        userBookLst[9].readCount += PlayerPrefs.GetInt("auto" + userId + StoryBook.YUMMY_SHAPES);
        // userBookLst[9].gameObject.AddComponent<AccuracyYummyShapes>();
        // userBookLst[9].bookAccuracy = userBookLst[1].GetComponent<BookAccuracy>();

        //MODULES

        userBookLst[0].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.FAVORITE_BOX);
        userBookLst[0].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.FAVORITE_BOX);
        userBookLst[0].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.FAVORITE_BOX);

        userBookLst[1].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.AFTER_THE_RAIN);
        userBookLst[1].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.AFTER_THE_RAIN);
        userBookLst[1].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.AFTER_THE_RAIN);

        userBookLst[2].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.ABC_CIRCUS);
        userBookLst[2].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.ABC_CIRCUS);
        userBookLst[2].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.ABC_CIRCUS);

        userBookLst[3].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.CHAT_WITH_MY_CAT);
        userBookLst[3].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.CHAT_WITH_MY_CAT);
        userBookLst[3].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.CHAT_WITH_MY_CAT);

        userBookLst[4].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.COLORS_ALL_MIXED_UP);
        userBookLst[4].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.COLORS_ALL_MIXED_UP);
        userBookLst[4].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.COLORS_ALL_MIXED_UP);

        userBookLst[5].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.JOEY_GO_TO_SCHOOL);
        userBookLst[5].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.JOEY_GO_TO_SCHOOL);
        userBookLst[6].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.JOEY_GO_TO_SCHOOL);

        userBookLst[6].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.SOUNDS_FANTASTIC);
        userBookLst[6].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.SOUNDS_FANTASTIC);
        userBookLst[6].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.SOUNDS_FANTASTIC);

        userBookLst[7].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.TINA_AND_JUN);
        userBookLst[7].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.TINA_AND_JUN);
        userBookLst[7].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.TINA_AND_JUN);

        userBookLst[8].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.WHAT_DID_YOU_SEE);
        userBookLst[8].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.WHAT_DID_YOU_SEE);
        userBookLst[8].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.WHAT_DID_YOU_SEE);

        userBookLst[9].playedCount += PlayerPrefs.GetInt(Module.WORD + userId + StoryBook.YUMMY_SHAPES);
        userBookLst[9].playedCount += PlayerPrefs.GetInt(Module.PUZZLE + userId + StoryBook.YUMMY_SHAPES);
        userBookLst[9].playedCount += PlayerPrefs.GetInt(Module.OBSERVATION + userId + StoryBook.YUMMY_SHAPES);


       
    }

    void Sort()
    {

        int tempCount;
        Sprite tempCover;
        // BookAccuracy tempBookAcc;

        for (int i = 0; i < userBookLst.Count; i++ )
        {
            for (int j = 0; j < userBookLst.Count - 1; j++ )
            {
                
                if((userBookLst[j].readCount + userBookLst[j].playedCount) < (userBookLst[j+1].readCount + userBookLst[j+1].playedCount))
                {
                    tempCover = userBookLst[j + 1].Cover.sprite;
                    tempCount = userBookLst[j + 1].readCount;
                    // tempBookAcc = userBookLst[j + 1].bookAccuracy;
                    //read count
                    userBookLst[j + 1].ReadCount = userBookLst[j].readCount;
                    userBookLst[j].ReadCount = tempCount;

                    //played count
                    tempCount = userBookLst[j + 1].playedCount;
                    userBookLst[j + 1].playedCount = userBookLst[j].playedCount;
                    userBookLst[j].playedCount = tempCount;

                    userBookLst[j + 1].Cover.sprite = userBookLst[j].Cover.sprite;
                    userBookLst[j].Cover.sprite = tempCover;

                    // userBookLst[j + 1].bookAccuracy = userBookLst[j].bookAccuracy;
                    // userBookLst[j].bookAccuracy = tempBookAcc;
                   
                }
            }
        }
        
        for (int i = 0; i < userBookLst.Count; i++)
        {
            print("PRINTO! " + userBookLst[i].CoverSprite.name);
            // if(userBookLst[i].CoverSprite.name.Equals("My Favorite box-Tablet cover")){
            //         userBookLst[i].gameObject.AddComponent<AccuracyFavoriteBox>();
            //         userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
            // }

            switch (userBookLst[i].CoverSprite.name)
            {
                case "ABC-Circus-TabletCover-6":
                    userBookLst[i].gameObject.AddComponent<AccuracyABC>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "After The Rain Page-tabletcover":
                    userBookLst[i].gameObject.AddComponent<AccuracyAfterTheRain>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Chat with my Cat p00 Cover":
                    userBookLst[i].gameObject.AddComponent<AccuracyChatWithCat>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Colors All Mixed Up-Cover-7":
                    userBookLst[i].gameObject.AddComponent<AccuracyColorsAllMixedUp>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Joey Goes to School-TabletCover":
                    userBookLst[i].gameObject.AddComponent<AccuracyJoeyGoesToSchool>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "My Favorite box-Tablet cover":
                    userBookLst[i].gameObject.AddComponent<AccuracyFavoriteBox>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Sounds Fantastic-TabletCover-8":
                    userBookLst[i].gameObject.AddComponent<AccuracySoundsFantastic>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Tina and Jun-TabletCover-9":
                    userBookLst[i].gameObject.AddComponent<AccuracyTinaAndJun>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "What Did You See - Cover":
                    userBookLst[i].gameObject.AddComponent<AccuracyWhatDidYouSee>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                case "Yummy-Shapes-tablet-cover-10":
                    userBookLst[i].gameObject.AddComponent<AccuracyYummyShapes>();
                    userBookLst[i].bookAccuracy = userBookLst[i].GetComponent<BookAccuracy>();
                    break;
                default:
                   
                    break;
            }
          

            userBookLst[i].TxtInfo.text = "Times Read: " + "<#ffc600>" + userBookLst[i].readCount.ToString() + "</color>";
            userBookLst[i].TxtInfo.text += "\n" + "Times Played: " + "<#ffc600>" + userBookLst[i].playedCount.ToString() + "</color>";
            userBookLst[i].TxtInfo.text += "\n" + "Accuracy: " + "<#ffc600>" + userBookLst[i].AccuracyRate.ToString() + "%" + "</color>";

            userBookLst[i].UTxtInfo.text = "Times Read: " + "<#ffc600>" + userBookLst[i].readCount.ToString() + "</color>";
            userBookLst[i].UTxtInfo.text += "\n" + "Times Played: " + "<#ffc600>" + userBookLst[i].playedCount.ToString() + "</color>";
            userBookLst[i].UTxtInfo.text += "\n" + ": " + "<#ffc600>" + userBookLst[i].AccuracyRate.ToString() + "%" + "</color>";

        }

        if (userBookLst[0].playedCount + userBookLst[0].readCount >= 5)
        {
            //print("TIFFANY! " + userBookLst[0].CoverSprite.name + userBookLst[0].playedCount + userBookLst[0].readCount);
            switch (userBookLst[0].CoverSprite.name)
            {
                case "ABC-Circus-TabletCover-6":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is ABC Circus Your child LOVES TO LEARN ABOUT NEW WORDS and could grow up to be a teacher or scholar! You might want to encourage your child to read and write more!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "After The Rain Page-tabletcover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is After the Rain this means that your child could grow up to be event planner or project manager";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Chat with my Cat p00 Cover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is Chat With My Cat! Your child is ARTISTIC  and could grow up to be a POET!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Colors All Mixed Up-Cover-7":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        "  favorite book is Colors All Mixed Up! Your child is ARTISTIC and could grow up to be an artist! You might want to encourage your child to draw or paint!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Joey Goes to School-TabletCover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is Joey Goes To School! Your child LIKES CONQUERING FEAR and have NEW EXPERIENCES and could grow up to be an explorer! You might want to encourage your child to try new things and go places!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "My Favorite box-Tablet cover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is My Favorite Box! Your child is IMAGINATIVE and could grow up to be a WRITER! You might want to encourage your child create and tell his/her own stories!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Sounds Fantastic-TabletCover-8":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is Sounds Fantastic Your child LOVES TO LISTEN TO SOUNDS and could grow up to be a composer! You might want to encourage your child to sing or learn a musical instrument!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Tina and Jun-TabletCover-9":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is Tina and Jun! Your child LOVES MUSIC and could grow up to be a Rock/Pop star! You might want to encourage your child to sing or learn a musical instrument!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "What Did You See - Cover":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is What did you See?  Your child likes to be SOCIABLE and could grow up to be a reporter! You might want to encourage your child to play and interact with other kids and other people!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                case "Yummy-Shapes-tablet-cover-10":
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(userId).Split(' ')[3] + "'s" +
                        " favorite book is YUMMY SHAPES! Your child is ARTISTIC and could grow up to be chef! You might want to encourage your child to do more creative and artistic activities!";
                    print(userBookLst[0].CoverSprite.name);
                    utoBubble.gameObject.SetActive(true);
                    break;
                default:
                    utoBubble.transform.GetChild(0).GetComponent<Text>().text = "";
                    break;
            }
        }
       
        
    }

   
    
}
