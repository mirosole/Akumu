using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Floor1");
    }
    
    public void QuitGame()
    {
        Debug.Log("Exit sucsesfull");
        Application.Quit();
    }
}