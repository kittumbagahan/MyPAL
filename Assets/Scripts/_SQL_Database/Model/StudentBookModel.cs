﻿using SQLite4Unity3d;

public class StudentBookModel {

	[PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int SectionId { get; set; }
    public int StudentId { get; set; }
    public int BookId { get; set; }
    public int ReadCount { get; set; }
    public int ReadToMeCount { get; set; }
    public int AutoReadCount { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={1}", Id, AutoReadCount);
    }
}
