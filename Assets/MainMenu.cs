using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public GameObject OptionMenu;
    public GameObject StartMenu;
    public GameObject InstructionsMenu;

    public AudioMixer audioMixer;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);  
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToOptionsMenu()
    {
        OptionMenu.SetActive(true);
        StartMenu.SetActive(false);
        InstructionsMenu.SetActive(false);
    }
    public void ToInstructionMenu()
    {
        InstructionsMenu.SetActive(true);
        StartMenu.SetActive(false);        
        OptionMenu.SetActive(false);
    }

    public void ToStartMenu()
    {
        StartMenu.SetActive(true);
        OptionMenu.SetActive(false);
        InstructionsMenu.SetActive(false);
    }

    //Allows the slider to control the volume
    public void SetVolume(float _volume)
    {
        audioMixer.SetFloat("Volume", _volume);
    }
}