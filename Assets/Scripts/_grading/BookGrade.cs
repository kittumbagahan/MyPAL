using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookGrade {

    public StudentModel student;
    BookModel book;
    public ModuleGrade wordGrade;
    public ModuleGrade observationGrade;

    public string Description()
    {
        return book.Description;
    }

    public BookGrade(BookModel book, StudentModel student)
    {
        this.book = book;
        this.student = student;
    }
}
