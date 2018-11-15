using SQLite4Unity3d;

public class SectionModel{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Description { get; set; }
    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Desc={1}", Id, Description);
    }
}
