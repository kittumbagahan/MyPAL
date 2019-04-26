using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _AssetBundleServer
{
    [Serializable]
    public class BookAndActivityData
    {
        [SerializeField]
        private string Name;
        [SerializeField]
        public BookModelJson book;
        [SerializeField]
        public List<ActivityModelJson> activities = new List<ActivityModelJson>();
    }
}