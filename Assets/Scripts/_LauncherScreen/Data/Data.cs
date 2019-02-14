using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace AppLauncher
{
    public class Data : MonoBehaviour
    {
        Button btnEdit;
        Text label;

        // Use this for initialization
        void Start()
        {
            //btnEdit = GetComponentInChildren<Button>();
            //label = GetComponentInChildren<Text>();
        }
        
        #region METHODS

        // Set string to label
        public void SetData(string text)
        {
            Debug.Log("Set data");

            btnEdit = GetComponentInChildren<Button>();
            label = GetComponentInChildren<Text>();

            label.text = text;
        }

        public void AddEvent(UnityAction action)
        {
            btnEdit.onClick.AddListener(action);
        }

        public void RemoveEvent()
        {
            btnEdit.onClick.RemoveAllListeners();
        }

        #endregion
    }
}
