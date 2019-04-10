using System;
using System.Collections.Generic;

namespace _AssetBundleServer
{
    [Serializable]
    public class BookAndActivityData
    {
        public BookModelJson book;
        public List<ActivityModelJson> lstActivity = new List<ActivityModelJson>();
    }
}