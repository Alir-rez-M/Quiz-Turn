using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool isGamePaused;
    [SerializeField] private GameObject pause;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (isGamePaused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
         

        
    }
    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
