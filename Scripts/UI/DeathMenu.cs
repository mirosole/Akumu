using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Try to change scene");
        SceneManager.LoadScene("Floor2");
    }

    public void ToMenu()
    {
        Debug.Log("Try to go to menu");
        SceneManager.LoadScene("Main Menu Scene");
    }
}
