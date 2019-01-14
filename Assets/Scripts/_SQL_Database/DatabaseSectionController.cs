using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;



public sealed class DatabaseSectionController : DatabaseController
{
    public delegate void Method();
    public Queue<Method> methods;

    public DatabaseSectionController() : base()
    {

    }


  

    public void CreateSectionDb(string dbName)
    {
        Debug.Log(Application.persistentDataPath);
        if (!File.Exists(DatabaseDirectory + "/" + dbName))
        {
            File.Create(DatabaseDirectory + "/" + dbName).Close();

        }
        else
        {
            // throw new System.Exception();
        }

    }

    public void RenameDb(string oldName, string newName)
    {
    
      try
      {
         File.Move (DatabaseDirectory + "/" + oldName, DatabaseDirectory + "/" + newName);
      }
        catch(IOException ex)
      {
         throw new IOException ("What happened? " + ex.Message);
      }
    }

    public void CreateSectionTables(string dbName)
    {
        DataService.Open(dbName);

        DataService._connection.CreateTable<SectionModel>();

        DataService._connection.CreateTable<StudentModel>();

        DataService._connection.CreateTable<BookModel>();

        DataService._connection.InsertAll(new[] {
                   new BookModel
                   {

                      Description = StoryBook.ABC_CIRCUS.ToString()
                   },
                    new BookModel
                   {

                      Description = StoryBook.AFTER_THE_RAIN.ToString()
                   },
                       new BookModel
                   {

                      Description = StoryBook.CHAT_WITH_MY_CAT.ToString()
                   },
                  new BookModel
                   {

                      Description = StoryBook.COLORS_ALL_MIXED_UP.ToString()
                   },
                     new BookModel
                   {

                      Description = StoryBook.FAVORITE_BOX.ToString()
                   },
                     new BookModel
                   {

                      Description = StoryBook.JOEY_GO_TO_SCHOOL.ToString()
                   },
                  new BookModel
                   {

                      Description = StoryBook.SOUNDS_FANTASTIC.ToString()
                   },
                     new BookModel
                   {

                      Description = StoryBook.TINA_AND_JUN.ToString()
                   },
                        new BookModel
                   {

                      Description = StoryBook.WHAT_DID_YOU_SEE.ToString()
                   },
                           new BookModel
                   {

                      Description = StoryBook.YUMMY_SHAPES.ToString()
                   }
             });

        #region ActivityModel
        DataService._connection.CreateTable<ActivityModel>();

        //NOTE: THE BOOK ID BASED ON THE BOOK TABLE CREATION

        //ABC-CIRCUS
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act2",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act2",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act2",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act2",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act2",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act4",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act6",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act5",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act5",
                    Module = "PUZZLE",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act1",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act1",
                    Module = "OBSERVATION",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act1",
                    Module = "OBSERVATION",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act1",
                    Module = "OBSERVATION",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 1,
                    Description = "ABCCircus_Act1",
                    Module = "OBSERVATION",
                    Set = 12
                },
            });

        //AFTER THE RAIN
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act1",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act1",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act1",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act1",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act1",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act2",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act3",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act3",
                    Module = "PUZZLE",
                    Set = 4
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act4",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act6",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act6",
                    Module = "OBSERVATION",
                    Set = 4
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act6",
                    Module = "OBSERVATION",
                    Set = 10
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act6",
                    Module = "OBSERVATION",
                    Set = 18
                },
                new ActivityModel
                {
                    BookId = 2,
                    Description = "afterTheRain_Act6",
                    Module = "OBSERVATION",
                    Set = 28
                },
            });

        //CHAT WITH MY CAT
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_2",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_2",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_2",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_2",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_2",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_3",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_4",
                    Module = "PUZZLE",
                    Set = 1
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_5",
                    Module = "PUZZLE",
                    Set = 2
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_6",
                    Module = "PUZZLE",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_1",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_1",
                    Module = "OBSERVATION",
                    Set = 1
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_1",
                    Module = "OBSERVATION",
                    Set = 2
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_1",
                    Module = "OBSERVATION",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 3,
                    Description = "chatWithCat_Act_1",
                    Module = "OBSERVATION",
                    Set = 4
                },
            });

        //COLORS ALL MIXED UP
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_7",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_7",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_7",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_7",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_7",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_2",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_3",
                    Module = "PUZZLE",
                    Set = 1
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_4",
                    Module = "PUZZLE",
                    Set = 2
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_5",
                    Module = "PUZZLE",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_6",
                    Module = "PUZZLE",
                    Set = 4
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_1",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_1",
                    Module = "OBSERVATION",
                    Set = 1
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_1",
                    Module = "OBSERVATION",
                    Set = 2
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_1",
                    Module = "OBSERVATION",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 4,
                    Description = "colorsAllMixedUp_Act_1",
                    Module = "OBSERVATION",
                    Set = 4
                },
            });

        //FAVORITE BOX
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act1_word",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act1_word",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act1_word",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act1_word",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act1_word",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act2_coloring",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act4",
                    Module = "PUZZLE",
                    Set = 1
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act5",
                    Module = "PUZZLE",
                    Set = 2
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favbox6_NEW",
                    Module = "PUZZLE",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act3_spotDiff",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act3_spotDiff",
                    Module = "OBSERVATION",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act3_spotDiff",
                    Module = "OBSERVATION",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act3_spotDiff",
                    Module = "OBSERVATION",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 5,
                    Description = "favBox_Act3_spotDiff",
                    Module = "OBSERVATION",
                    Set = 12
                },
            });

        //JOEY GOES TO SCHOOL
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act1",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act1",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act1",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act1",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act1",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act2",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act4",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act5",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act6",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act3",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act3",
                    Module = "OBSERVATION",
                    Set = 4
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act3",
                    Module = "OBSERVATION",
                    Set = 10
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act3",
                    Module = "OBSERVATION",
                    Set = 18
                },
                new ActivityModel
                {
                    BookId = 6,
                    Description = "JoeyGoesToSchool_Act3",
                    Module = "OBSERVATION",
                    Set = 28
                },
            });

        //SOUNDS FANTASTIC
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act4",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act4",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act4",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act4",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act4",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "soundsFantastic_Act1",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 7,
                    Description = "soundsFantastic_Act2",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act5",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act5",
                    Module = "PUZZLE",
                    Set = 4
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act3",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act6",
                    Module = "OBSERVATION",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act7",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act8",
                    Module = "OBSERVATION",
                    Set = -1
                },
                new ActivityModel
                {
                    BookId = 7,
                    Description = "SoundsFantastic_Act8",
                    Module = "OBSERVATION",
                    Set = 2
                },
            });

        //TINA AND JUN
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act1",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act2",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act2",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act2",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act2",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act4",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act5",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act6",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act7",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act3",
                    Module = "OBSERVATION",
                    Set = -1
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act3",
                    Module = "OBSERVATION",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act3",
                    Module = "OBSERVATION",
                    Set = 7
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act3",
                    Module = "OBSERVATION",
                    Set = 11
                },
                new ActivityModel
                {
                    BookId = 8,
                    Description = "TinaAndJun_Act3",
                    Module = "OBSERVATION",
                    Set = 15
                },
            });

        //WHAT DID YOU SEE
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct7",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct7",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct7",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct7",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct7",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "whatDidYaSee_act1",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct4",
                    Module = "PUZZLE",
                    Set = 1
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct6",
                    Module = "PUZZLE",
                    Set = 2
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct3",
                    Module = "PUZZLE",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "whatDidYaSee_act2",
                    Module = "OBSERVATION",
                    Set = -1
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "whatDidYaSee_act2",
                    Module = "OBSERVATION",
                    Set = 2
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "whatDidYaSee_act2",
                    Module = "OBSERVATION",
                    Set = 5
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct5",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 9,
                    Description = "WhatDidYouSeeAct8",
                    Module = "OBSERVATION",
                    Set = 0
                },
            });

        //YUMMY SHAPES
        DataService._connection.InsertAll(new[] {
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_1",
                    Module = "WORD",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_1",
                    Module = "WORD",
                    Set = 3
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_1",
                    Module = "WORD",
                    Set = 6
                },
                new ActivityModel
                {
                   BookId = 10,
                    Description = "yummyShapes_Act_1",
                    Module = "WORD",
                    Set = 9
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_1",
                    Module = "WORD",
                    Set = 12
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_4",
                    Module = "PUZZLE",
                    Set = 0
                },
                 new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_5",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_6",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_7",
                    Module = "PUZZLE",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_2",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_2",
                    Module = "OBSERVATION",
                    Set = 3
                },
                new ActivityModel
                {
                   BookId = 10,
                    Description = "yummyShapes_Act_3",
                    Module = "OBSERVATION",
                    Set = 0
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_3",
                    Module = "OBSERVATION",
                    Set = 4
                },
                new ActivityModel
                {
                    BookId = 10,
                    Description = "yummyShapes_Act_3",
                    Module = "OBSERVATION",
                    Set = 8
                },
            });

        #endregion ActivityModel

        DataService._connection.CreateTable<StudentActivityModel>();

        DataService._connection.CreateTable<StudentBookModel>();

        DataService.Close();

      
    }


}
