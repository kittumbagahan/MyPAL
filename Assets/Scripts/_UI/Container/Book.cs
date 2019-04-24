using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Book : MonoBehaviour {

	public static Book instance;

	RawImage rawImage;

	[SerializeField]
	Texture[] bookCover;

	[SerializeField]
	Text bookDescription;

	[SerializeField]
	string[] _bookDescription;

	// Use this for initialization
	void Start () {
		instance = this;
		rawImage = GetComponent<RawImage>();
	}

	public void	BookCover(int index)
	{
		rawImage.texture = bookCover[index];
	}

	public void BookDescription(int index)
	{
		bookDescription.text = _bookDescription[index];
	}
}
