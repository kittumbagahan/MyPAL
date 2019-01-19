using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookModelJson  {

    public int Id;
    public string Description;

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
