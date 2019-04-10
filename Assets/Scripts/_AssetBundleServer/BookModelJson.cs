using System;

namespace _AssetBundleServer
{
    [Serializable]
    public class BookModelJson
    {
        public string Description;

        public int Id;

        public BookModelJson(int id, string desc)
        {
            Id = id;
            Description = desc;
        }

        public override string ToString()
        {
            return string.Format("[Book: Id={0}, Desc={1}", Id, Description);
        }
    }
}