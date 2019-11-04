using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    public GameObject MainMenu;
    public AudioMixer audioMixer;

    public void BackButton()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }

    //Allows the slider to control the volume
    public void SetVolume(float _volume)
    {
        audioMixer.SetFloat("Volume", _volume);
    }

}
