using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        
        
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
