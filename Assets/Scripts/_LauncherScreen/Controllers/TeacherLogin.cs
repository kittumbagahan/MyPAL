using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AppLauncher
{
    public class TeacherLogin : MonoBehaviour
    {

        [SerializeField]
        InputField inputTeacherPassword;

        [SerializeField]
        Button btnLogin;

        // Use this for initialization
        void Start()
        {
            btnLogin.onClick.AddListener(Login);
        }

        void Login()
        {
            ResetPasswordModel resetPasswordModel = null;
            DataService.Open("system/admin.db");
            AdminPasswordModel admin = DataService._connection.Table<AdminPasswordModel>().Where(x => x.Id == 1).FirstOrDefault();

            if (inputTeacherPassword.text.Equals(admin.Password))
            {
                UserRestrictionController.ins.Restrict(0);
                StoryBookSaveManager.ins.activeUser = "teacher";
                StoryBookSaveManager.ins.activeUser_id = -1;

                // UI
                GetComponent<UIControllers>().ShowTeacherDashboard();

                //show section selection
            }
            else if (resetPasswordModel != SystemPassword(inputTeacherPassword.text))
            {
                UserRestrictionController.ins.Restrict(0);
                StoryBookSaveManager.ins.activeUser = "teacher";
                StoryBookSaveManager.ins.activeUser_id = -1;
                MessageBox.ins.ShowOk("Admin password has been reset to 1234", MessageBox.MsgIcon.msgInformation, null);

                // UI
                GetComponent<UIControllers>().ShowTeacherDashboard();
            }
            else
            {
                MessageBox.ins.ShowOk("Access denied!", MessageBox.MsgIcon.msgError, null);
            }

            DataService.Close();
        }

        private ResetPasswordModel SystemPassword(string input)
        {
            //DataService ds = new DataService();
            DataService.Open("system/admin.db");
            ResetPasswordModel model = DataService._connection.Table<ResetPasswordModel>().Where(x => x.Used == false && x.SystemPasscode == input).FirstOrDefault();
            if (model != null)
                model.Used = true;
            else
                return null;

            AdminPasswordModel admin = DataService._connection.Table<AdminPasswordModel>().Where(x => x.Id == 1).FirstOrDefault();
            admin.Password = "1234";
            DataService._connection.Update(model);
            DataService._connection.Update(admin);

            DataService.Close();

            return model;
        }
    }
}