using System;
using UnityEngine;

namespace _AssetBundleServer
{
    [Serializable]
    public class ActivityModelJson
    {
        public string Name;
        
        public int BookId;
        
        public string Description
        {
            get { return Name; }
        }
        
        [HideInInspector]
        public int Id; //Auto Inc

        public Module Module;
        public int Set;

        public override string ToString()
        {
            return string.Format("[ActivityModel: Id={0}, BookId={1}, Description={2}, Module={3}, Set={4}",
                Id, BookId, Description, Module, Set);
        }
    }
}