using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using _AssetBundleServer;

public class BookAndModelDataTests {

	[Test]
	public void Json()
	{
		List<BookAndActivityData> bookList = new List<BookAndActivityData>();
		
		var bookAndActivity = new BookAndActivityData();
		bookAndActivity.book = new BookModelJson()
		{
			Id = 11,
			Name = StoryBook.HUGIS_KAY_SARAP
		};
		bookAndActivity.activities.Add(new ActivityModelJson()
		{
			BookId = 11,
			Name = "book_test_1_Act1",
			Module = Module.WORD,
			Set = 0
		});
		bookAndActivity.activities.Add(new ActivityModelJson()
		{
			BookId = 11,
			Name = "book_test_1_Act1",
			Module = Module.WORD,
			Set = 3
		});
		bookAndActivity.activities.Add(new ActivityModelJson()
		{
			BookId = 11,
			Name = "book_test_1_Act1",
			Module = Module.WORD,
			Set = 6
		});
		bookAndActivity.activities.Add(new ActivityModelJson()
		{
			BookId = 11,
			Name = "book_test_1_Act1",
			Module = Module.WORD,
			Set = 9
		});
		bookAndActivity.activities.Add(new ActivityModelJson()
		{
			BookId = 11,
			Name = "book_test_1_Act1",
			Module = Module.WORD,
			Set = 12
		});
		
		bookList.Add(bookAndActivity);

		var jsonString = JsonMapper.ToJson(bookList);
		jsonString = "";
		Debug.Log(jsonString);

		List<BookAndActivityData> bookList2 = JsonMapper.ToObject<List<BookAndActivityData>>(jsonString);
		
		if(bookList2 != null)
			Debug.Log("count " + bookList2.Count);
		else		
			Debug.Log("null");		
		
//		Debug.Log(bookList2[0].book.Description);
//		Debug.Log(bookList2[0].lstActivity[0].Description);
	}
}
