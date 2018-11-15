using SQLite4Unity3d;

public class StudentActivityModel  {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int SectionId { get; set; }
    public int StudentId { get; set; }
    public int BookId { get; set; }
    public int ActivityId { get; set; }
    public int Score { get; set; }
    public int PlayCount { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={1}", Id, Score);
    }


}
