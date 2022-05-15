using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    AudioSource menuMusic;

    void Start()
    {
        menuMusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        menuMusic.volume = MainMenu.gameVolume;
    }
}
