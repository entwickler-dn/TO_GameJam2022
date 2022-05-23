using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Toggle skipTut;
    public static bool skipTutorial = true;

    [Header("Options")]
    public GameObject optionsPanel;
    public Slider generalVolume;
    public Toggle muteAll;
    AudioSource menuMusic;

    public static float gameVolume = 0.6f;
    public static bool mutedAll = false;

    void Start()
    {
        Time.timeScale = 1f;
        menuMusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (mutedAll)
        {
            gameVolume = 0;
        }
        else
        {
            gameVolume = generalVolume.value;
        }
        
        menuMusic.volume = gameVolume;

        mutedAll = muteAll.isOn;        
    }

    public void PlayButton()
    {
        if(skipTutorial)
        {
            SceneManager.LoadScene("MainGame");
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
        
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Buttoncito()
    {
        skipTutorial = !skipTutorial;
    }
}
