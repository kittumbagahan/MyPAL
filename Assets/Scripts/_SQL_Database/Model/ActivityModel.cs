using SQLite4Unity3d;

public class ActivityModel {


    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int BookId { get; set; }
    public string Description { get; set; }
    public string Module { get; set; }
    public int Set { get; set; }
}
