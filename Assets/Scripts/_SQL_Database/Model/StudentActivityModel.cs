﻿using SQLite4Unity3d;

public class StudentActivityModel  {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int SectionId { get; set; }
    public int StudentId { get; set; }
    public int BookId { get; set; }
    public int ActivityId { get; set; }
    public string Grade { get; set; }
    public int PlayCount { get; set; }

    public override string ToString()
    {
        return string.Format("[StudentActivityModel: Id={0}, ActivityId={1} Grade={2}, PlayCount={3}", Id, ActivityId, Grade, PlayCount);
    }


}
