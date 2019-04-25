using System;

namespace _AssetBundleServer
{
    [Serializable]
    public class BookModelJson
    {
        public string Description { get; set; }

        public int Id { get; set; }               
        
        
        public override string ToString()
        {
            return string.Format("[Book: Id={0}, Desc={1}", Id, Description);
        }
    }
}