using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    private bool isPaused = false;
    public MonoBehaviour PlayerContoller;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        
        // Disable camera movement
        if (PlayerContoller != null)
            PlayerContoller.GetComponent<PlayerMovement>().isPaused = true;
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        
        // Enable camera movement
        if (PlayerContoller != null)
            PlayerContoller.GetComponent<PlayerMovement>().isPaused = false;
        
        // Block cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ToMenu()
    {
        Debug.Log("Going to menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu Scene");
    }

    public void ToGame()
    {
        Debug.Log("Restarting level");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
