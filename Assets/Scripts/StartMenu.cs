using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingsMenu;

    public TextMeshProUGUI sensText;
    public Slider sens;

    public TextMeshProUGUI audText;
    public Slider aud;

    private void Start()
    {
        settingsMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    private void Update()
    {
        

        sensText.text = $"Sensitivity: {sens.value}";
        audText.text = $"Audio: {aud.value}%";
        MusicController.volume = aud.value / 100;
    }

    public void startGame()
    {
        PlayerCam.sensX = sens.value * 100;
        PlayerCam.sensY = sens.value * 100;
        SceneManager.LoadScene(1);
    }

    public void showSettingsMenu()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void showStartMenu()
    {
        settingsMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void quit()
    {
        Application.Quit();
    }
}
