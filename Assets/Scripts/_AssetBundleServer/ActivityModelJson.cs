using System;

namespace _AssetBundleServer
{
    [Serializable]
    public class ActivityModelJson
    {
        public int BookId { get; set; }
        public string Description { get; set; }

        public int Id; //Auto Inc
        public string Module { get; set; }
        public int Set { get; set; }       

        public override string ToString()
        {
            return string.Format("[ActivityModel: Id={0}, BookId={1}, Description={2}, Module={3}, Set={4}",
                Id, BookId, Description, Module, Set);
        }
    }
}