using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerView : MonoBehaviour
{
	private static GameObject _container;
	
	[SerializeField] private GameObject studentBlock;
	[SerializeField] private GameObject parent;
	private Toggle _toggleMasterList;			
	private GameObject _studentContainer;

	public static Action OnOpenMasterList;
	public static Action OnCloseMasterList;

	public GameObject StudentBlock
	{
		get { return studentBlock; }
	}
	
	public GameObject Parent
	{
		get { return parent; }
	}
	
	// Use this for initialization
	void Start ()
	{
		SetUp();
	}

	private void SetUp()
	{
		GetReferences();
		SetBindings();
	}

	private void GetReferences()
	{
		_container = transform.GetChild(0).gameObject;
		_toggleMasterList = transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Toggle>();
		_studentContainer = transform.GetChild(0).transform.GetChild(1).gameObject;

		_toggleMasterList.isOn = true;				
	}

	private void SetBindings()
	{
		_toggleMasterList.onValueChanged.AddListener(ToggleMasterList);
	}

	private void ToggleMasterList(bool isOpen)
	{
		_studentContainer.SetActive(isOpen);
	}		
	
	public static void OpenMasterListUi()
	{
		if(_container != null)
			_container.SetActive(true);

		if (OnOpenMasterList != null)
			OnOpenMasterList();
	}
	
	public static void CloseMasterList()
	{
		if(_container != null)
			_container.SetActive(false);

		if (OnCloseMasterList != null)
			OnCloseMasterList();
	}
}
