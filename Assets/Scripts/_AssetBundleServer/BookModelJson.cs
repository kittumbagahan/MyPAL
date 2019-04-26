using System;
using UnityEngine;

namespace _AssetBundleServer
{
    [Serializable]
    public class BookModelJson
    {
        public StoryBook Name;
        public StoryBook Description {get { return Name; }}

        [HideInInspector] public int Id;               
        
        
        public override string ToString()
        {
            return string.Format("[Book: Id={0}, Desc={1}", Id, Description);
        }
    }
}