using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStudentName{

    public void DeleteActivity(string user)
    {
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act2_coloring" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act4" + Module.PUZZLE.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act5" + Module.PUZZLE.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favbox6_NEW" + Module.PUZZLE.ToString() + "3");

        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_3" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_4" + Module.PUZZLE.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_5" + Module.PUZZLE.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_6" + Module.PUZZLE.ToString() + "3");

        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION.ToString() + "4");

        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act2" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE.ToString() + "4");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act4" + Module.PUZZLE.ToString() + "0");

        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "4");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "10");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "18");
        PlayerPrefs.DeleteKey(user + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION.ToString() + "28");

        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_2" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_3" + Module.PUZZLE.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_4" + Module.PUZZLE.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_5" + Module.PUZZLE.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_6" + Module.PUZZLE.ToString() + "4");

        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION.ToString() + "4");

        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act1" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct4" + Module.PUZZLE.ToString() + "1");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct6" + Module.PUZZLE.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct3" + Module.PUZZLE.ToString() + "3");

        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "-1");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "2");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION.ToString() + "5");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct5" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct8" + Module.OBSERVATION.ToString() + "0");

        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act4" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act6" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE.ToString() + "3");

        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act2" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act4" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act6" + Module.PUZZLE.ToString() + "0");

        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "4");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "10");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "18");
        PlayerPrefs.DeleteKey(user + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION.ToString() + "28");

        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act1" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act2" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE.ToString() + "4");

        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act3" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act6" + Module.OBSERVATION.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act7" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION.ToString() + "-1");
        PlayerPrefs.DeleteKey(user + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION.ToString() + "2");

        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act1" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD.ToString() + "9");

        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act4" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act6" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act7" + Module.PUZZLE.ToString() + "0");

        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "-1");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "7");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "11");
        PlayerPrefs.DeleteKey(user + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION.ToString() + "15");

        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "6");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "9");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD.ToString() + "12");

        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_4" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_5" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_6" + Module.PUZZLE.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_7" + Module.PUZZLE.ToString() + "0"); //warning

        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION.ToString() + "3");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "0");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "4");
        PlayerPrefs.DeleteKey(user + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION.ToString() + "8");

    }
    public void DeleteBooksUsage(string user)
    {
        // string key = "read" + StoryBookSaveManager.instance.User + StoryBookSaveManager.instance.selectedBook;
        //print("IU");
        foreach (StoryBook val in Enum.GetValues(typeof(StoryBook)))
        {
            PlayerPrefs.DeleteKey("read" + user + val.ToString());
            PlayerPrefs.DeleteKey("readItTome" + user + val.ToString());
            PlayerPrefs.DeleteKey("auto" + user + val.ToString());
        }
    }
    public void DeleteActivityUsage(string user)
    {
        //string key = btnAct.Mode + StoryBookSaveManager.instance.User + StoryBookSaveManager.instance.selectedBook;
        foreach (StoryBook val in Enum.GetValues(typeof(StoryBook)))
        {
            PlayerPrefs.DeleteKey(Module.WORD + user + val.ToString());
            PlayerPrefs.DeleteKey(Module.PUZZLE + user + val.ToString());
            PlayerPrefs.DeleteKey(Module.OBSERVATION + user + val.ToString());
        }
    }

}
