using System;
using UnityEngine;
using UnityEngine.UI;

namespace _UI
{
	public class ServerView : MonoBehaviour
	{
		public static ServerView Instance;
		
		private GameObject _container;
	
		[SerializeField] private GameObject studentBlock;
		[SerializeField] private GameObject parent;
		private Toggle _toggleMasterList;			
		private GameObject _studentContainer;

		public Action OnOpenMasterList;
		public Action OnCloseMasterList;


		[SerializeField] private Sprite _online;
		[SerializeField] private Sprite _offline;

		[SerializeField] private InputField inputSearchStudent;
		
		public Sprite Online
		{
			get { return _online; }
		}

		public Sprite Offline
		{
			get { return _offline; }
		}

		public InputField InputSearchStudent
		{
			get { return inputSearchStudent; }
		}

		void Awake()
		{
			if (Instance == null)				
				Instance = this;
		}		

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
	
		public void OpenMasterListUi()
		{
			if(_container != null)
				_container.SetActive(true);

			if (OnOpenMasterList != null)
				OnOpenMasterList();
		}
	
		public void CloseMasterList()
		{
			if(_container != null)
				_container.SetActive(false);

			if (OnCloseMasterList != null)
				OnCloseMasterList();
		}				
	}
}
