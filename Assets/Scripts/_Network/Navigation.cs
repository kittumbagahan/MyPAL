using System;
using UnityEngine.SceneManagement;

public class Navigation
{
    private readonly Action _quit;

    public Navigation(Action quit)
    {
        _quit = quit;
    }
    
    public void LoadBookShelf()
    {
        Quit();        
        EmptySceneLoader.ins.sceneToLoad = "BookShelf";
        SceneManager.LoadScene("empty");
    }

    public void Back()
    {
        Quit();
        SceneManager.LoadScene("Admin");
    }

    public void Quit()
    {
        if (_quit != null)
            _quit();
    }
}