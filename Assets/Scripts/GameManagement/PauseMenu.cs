using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    
    public void Pause(){
        Time.timeScale=0f;
        pauseMenu.SetActive(true);
    }
    
    public void Resume(){
        Time.timeScale=1f;
        pauseMenu.SetActive(false);
    }
    
    public void MainMenu(){
        Time.timeScale=1f;
        SceneManager.LoadScene("StartMenu");
    }
    
}
