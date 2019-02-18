using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaintenanceView : MonoBehaviour {

    [SerializeField]
    Transform transformParentContainer;

    [SerializeField]
    Toggle toggleSection,
                toggleStudent;

    [SerializeField]
    Button btnAdd;

    [SerializeField]
    GameObject data, // the data inside the container of selected tab
                    sectionPanel, // panel for section add, edit
                    studentPanel; // panel for student add, edit

    public Transform TransformParentContainer
    {
        get { return transformParentContainer; }
    }

    public Toggle ToggleSection
    {
        get { return toggleSection; }
    }

    public Toggle ToggleStudent
    {
        get { return toggleStudent; }
    }

    public Button BtnAdd
    {
        get { return btnAdd; }
    }

    public GameObject Data
    {
        get { return data; }
    }

    public GameObject SectionPanel
    {
        get { return sectionPanel; }
    }

    public GameObject StudentPanel
    {
        get { return studentPanel; }
    }
}
