using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavoriteBoxWordActivity : WordActivityBaseManager {
	private void Start()
	{
		Initialize();
		SetActivityIndex();
	}

	private void SetActivityIndex()
	{
		switch(wordGameManager.SetIndex)
		{
			case 0:
				ScoreManager.ins.maxMove = 5;
				break;
			case 3: 
				ScoreManager.ins.maxMove = 7;
				break;
			case 6: 
				ScoreManager.ins.maxMove = 6;
				break;
			case 9: 
				ScoreManager.ins.maxMove = 8;
				break;
			case 12: 
				ScoreManager.ins.maxMove = 8;
				break;
		}
	}
}
