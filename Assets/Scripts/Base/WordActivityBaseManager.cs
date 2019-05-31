using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordActivityBaseManager : MonoBehaviour {
	
	[SerializeField]
	protected AudioClip clipFit, clipWrong, clipDrag, clipSolved;      
	protected AudioSource AudSrc;	
	[SerializeField]
	protected WordGameManager wordGameManager;
	
	// Use this for initialization
	protected void Initialize () {
		WordGameManager.OnGenerate += ResizeItems;
		WordGameManager.OnGenerate += SetSlotsValue;
		WordGameManager.OnGenerate += DisableGroupLettersSlotImage;
       
		AudSrc = GetComponent<AudioSource>();
		wordGameManager.SetIndex = SaveTest.Set;
	}		
	
	private void ResizeItems()
	{
		RectTransform rect;
		Text txt;
        
		for(int i=0; i<InventoryManager.ins.items.Count; i++)
		{
			rect = InventoryManager.ins.items[i].GetComponent<RectTransform>();
			rect.SetHeight(70);
			rect.SetWidth(70);
			txt = InventoryManager.ins.items[i].transform.GetChild(0).GetComponent<Text>();
			txt.resizeTextMaxSize = 70;
		}
	}
	
	private void SetSlotsValue()
	{
		SlotStringValue slot = null;

		for(int i=0; i<WordGameManager.ins.groupClue.transform.childCount; i++)
		{
			slot = WordGameManager.ins.groupClue.transform.GetChild(i).GetComponent<SlotStringValue>();
			slot.strAlphabetValue = WordGameManager.ins.Word[i].ToString();           
		}
    
		Item.OnInsert += Insert;
		Item.OnBeginDrag += BeginDrag;
		Item.OnReturn += WrongInsert;
	}
	
	private void Insert(Transform parent, Transform item)
	{
		SlotStringValue slot = null;
		Text txt = null;
		slot = parent.GetComponent<SlotStringValue>();
		txt = item.GetChild(0).GetComponent<Text>();
        
		if (txt.text != slot.strAlphabetValue)
		{
			item.GetComponent<Item>().Return();
			item.GetComponent<Item>().RecentParent = item.GetComponent<Item>().StartOrigin;
			parent.GetComponent<Slot>().CheckSlot();          
		}
		else {
			item.GetComponent<Item>().Locked = true;
			if (wordGameManager.WordSolved())
			{
				AudSrc.PlayOneShot(clipSolved);
			}
			else {
				AudSrc.PlayOneShot(clipFit); 
			}
            
		}
	}
	
	private void BeginDrag(GameObject obj)
	{
		AudSrc.PlayOneShot(clipDrag);
		ScoreManager.ins.IncNumOfMoves();
	}
	
	private void WrongInsert(Transform t)
	{
		AudSrc.PlayOneShot(clipWrong);
		print("wrong");
		ScoreManager.ins.IncNumOfMistakes();
	}
	
	private void DisableGroupLettersSlotImage()
	{
		for (int i = 0; i < WordGameManager.ins.groupLetters.transform.childCount; i++)
		{
			WordGameManager.ins.groupLetters.transform.GetChild(i).GetComponent<Image>().enabled = false;
		}
	}
}
