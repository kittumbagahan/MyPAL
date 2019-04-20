using System;
using UnityEngine.SceneManagement;

public class AdminNavigation
{
    public void LoadBookShelf(Action quit)
    {
        if (quit != null)
            quit();
        SceneManager.LoadScene ("BookShelf");
    }

    public void Back(Action quit)
    {
        if (quit != null)
            quit();
        SceneManager.LoadScene("Admin");
    }

    void NoNetwork_Message()
    {
        MessageBox.ins.ShowOk("Not connected to a network, please check your wifi.", MessageBox.MsgIcon.msgInformation, null);
    }
}