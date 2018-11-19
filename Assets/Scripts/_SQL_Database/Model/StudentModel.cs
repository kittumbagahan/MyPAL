using SQLite4Unity3d;

public class StudentModel {

	[PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int SectionId { get; set; }
    public string Givenname { get; set; }
    public string Middlename { get; set; }
    public string Lastname { get; set; }
    public string Nickname { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={2}", Id, Givenname);
    }
}
