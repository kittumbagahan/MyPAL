using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppLauncher
{
    public class UIControllers : MonoBehaviour
    {

        [SerializeField]
        GameObject _PanelTeacherLogin,
                    _PanelTeacherDashboard;

        // Use this for initialization
        void Start()
        {

        }

        public void ShowTeacherDashboard()
        {
            _PanelTeacherDashboard.SetActive(true);
            _PanelTeacherLogin.SetActive(false);
        }
    }
}
