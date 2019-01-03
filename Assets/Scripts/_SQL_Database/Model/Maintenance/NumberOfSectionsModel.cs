using SQLite4Unity3d;

public class NumberOfSectionsModel {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int MaxSection { get; set; }
    //get existing playerprefs and set this attrib to its value


    public override string ToString()
    {
        return string.Format("[NumberOfSectionsModel: Id={0}, MaxSection={1}", Id, MaxSection);
    }
}
