using SQLite4Unity3d;

public class StudentBookModel {

	[PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public int SectionId { get; set; }
    public int StudentId { get; set; }
    public int BookId { get; set; }
    public int ReadCount { get; set; }
    public int ReadToMeCount { get; set; }
    public int AutoReadCount { get; set; }

    public override string ToString()
    {
        return string.Format("[StudentBookModel: Id={0}, AutoReadCount={1}, ReadToMeCount={2}, ReadCount={3}", Id, AutoReadCount, ReadToMeCount, ReadCount);
    }
}
