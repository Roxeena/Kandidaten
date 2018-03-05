using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Toggle MultiplayerToggle;
    public Toggle MouseToggle;

    private void Start()
    {
        MultiplayerToggle.isOn = GameValues.IsMultiplayer;
        MouseToggle.isOn = GameValues.IsMouse;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("main");
    }

    public void SetMultiplayer(bool isOn)
    {
        GameValues.IsMultiplayer = isOn;
    }

    public void SetMouse(bool isOn)
    {
        GameValues.IsMouse = isOn;
    }
}