using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Restart button functions
    public void Restart(){
        SceneManager.LoadScene("SampleScene");
    }
     
    //MainMenu button functions
    public void StartMenu(){
        SceneManager.LoadScene("StartMenu");
    }
}
