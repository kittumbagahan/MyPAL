using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FavoriteBox_Act1_Manager : MonoBehaviour {

    [SerializeField]
    AudioClip clipFit, clipWrong, clipDrag, clipSolved;      
    AudioSource _audSrc;
    int _slotIndex;
    [SerializeField]
    WordGameManager wordGameManager;

	void Start () {
        WordGameManager.OnGenerate += ResizeItems;
        WordGameManager.OnGenerate += SetSlotsValue;
        WordGameManager.OnGenerate += DisableGroupLettersSlotImage;
       
        _audSrc = GetComponent<AudioSource>();
		wordGameManager.SetIndex = SaveTest.Set;

		if(StoryBookSaveManager.ins.selectedBook == StoryBook.FAVORITE_BOX)
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
		else if(StoryBookSaveManager.ins.selectedBook == StoryBook.WHAT_DID_YOU_SEE)
		{
			switch(wordGameManager.SetIndex)
			{
			case 0:
				ScoreManager.ins.maxMove = 6;
				break;
			case 3: 
				ScoreManager.ins.maxMove = 7;
				break;
			case 6: 
				ScoreManager.ins.maxMove = 5;
				break;
			case 9: 
				ScoreManager.ins.maxMove = 5;
				break;
			case 12: 
				ScoreManager.ins.maxMove = 5;
				break;

			default: 
				break;
			}
		}

		ScoreManager.ins.AW();
    }

    void DisableItemsImage()
    {
        Image img = null;
        for (int i = 0; i < InventoryManager.ins.items.Count; i++)
        {
            img = InventoryManager.ins.items[i].GetComponent<Image>();
            img.enabled = false;
        }
    }


    void ResizeItems()
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

    void DisableGroupLettersSlotImage()
    {
        for (int i = 0; i < WordGameManager.ins.groupLetters.transform.childCount; i++)
        {
            WordGameManager.ins.groupLetters.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }   

    void SetSlotsValue()
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

    void Insert(Transform parent, Transform item)
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
                _audSrc.PlayOneShot(clipSolved);
            }
            else {
                _audSrc.PlayOneShot(clipFit); 
            }
            
        }
    }

    void WrongInsert(Transform t)
    {
        _audSrc.PlayOneShot(clipWrong);
        print("wrong");
		ScoreManager.ins.IncNumOfMistakes();
    }

    void BeginDrag(GameObject obj)
    {
        _audSrc.PlayOneShot(clipDrag);
		ScoreManager.ins.IncNumOfMoves();
    }
}
