using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is to reference asset bundle information when switching scenes
//
public static class AssetBundleInfo  {

	public static class BookScene
    {
        public static bool isAssetBundle;
        public static string urlKey;
        public static string versionKey;
        public static string name;
        public static int bookId;
    }

    public static class ActivityScene
    {
        public static bool isAssetBundle;
        public static string urlKey;
        public static string versionKey;
        public static string name;
        public static int activityId;
    }
}
