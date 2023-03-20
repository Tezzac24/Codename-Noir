using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    //play game
    public void Play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    //quit game
    public void Quit(){
        Application.Quit();
        Debug.Log("Player has quit");
    }
}
