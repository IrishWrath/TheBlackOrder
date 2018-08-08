using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    
    public string sceneName;
    public string helpSceneName;

    public void Play()
	{
        SceneManager.LoadScene(sceneName);
	}

    public void Help()
    {
        SceneManager.LoadScene(helpSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
