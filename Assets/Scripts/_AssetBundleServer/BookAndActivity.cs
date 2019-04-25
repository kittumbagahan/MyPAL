using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _AssetBundleServer;

public class BookAndActivity {

	public static void NewBookAndActivity(List<BookAndActivityData> bookAndActivityDataList)
	{
		DataService.Open("system/admin.db");
		
		var sectionsCount = DataService._connection.Table<AdminSectionsModel>().Count();
		
		DataService.Close();
		
		for (var index = 1; index <= sectionsCount; index++)
		{			
			DataService.Open("system/admin.db");
			
			var sectionName = FindAdminSectionsModel(index);
			
			Debug.Log(sectionName);
			
			DataService.Close();
			
			AddNewBook(bookAndActivityDataList, sectionName);
		}				
	}

	private static string FindAdminSectionsModel(int i)
	{
		return DataService._connection.Table<AdminSectionsModel>().Where(x => x.Id == i)
			.FirstOrDefault().Description;
	}

	private static void AddNewBook(IEnumerable<BookAndActivityData> bookAndActivityDataList, string sectionDbName)
	{
		DataService.Open(sectionDbName + ".db");

		foreach (var bookAndActivityData in bookAndActivityDataList)
		{
			var bookModel = FindBookAndActivityData(bookAndActivityData);
		
			if (bookModel == null)
			{
				NewBook(bookAndActivityData);
			}
			else
			{
				Debug.Log("Book description already exist.");
				//throw new System.Exception("ERROR!");
			}			
		}
				
		DataService.Close();
	}

	private static void NewBook(BookAndActivityData bookAndActivityData)
	{		
		DataService._connection.Insert(NewBookModel(bookAndActivityData));

		var bookModel = FindBookAndActivityData(bookAndActivityData);

		foreach (var activity in bookAndActivityData.lstActivity)
		{
			var activityModel = new ActivityModel
			{
				BookId = bookModel.Id, Description = activity.Description, Module = activity.Module, Set = activity.Set
			};

			DataService._connection.Insert(activityModel);
		}
	}

	private static BookModel FindBookAndActivityData(BookAndActivityData bookAndActivityData)
	{
		return DataService._connection.Table<BookModel>()
			.Where(x => x.Description == bookAndActivityData.book.Description)
			.FirstOrDefault();
	}

	private static BookModel NewBookModel(BookAndActivityData bookAndActivityData)
	{					
		var bookModel = new BookModel()
		{
			Description = bookAndActivityData.book.Description
		};
		
		return bookModel;
	}
}
