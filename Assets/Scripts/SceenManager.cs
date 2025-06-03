using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceenManager : MonoBehaviour
{
    public void RestartSceen()
    {
        SceneManager.LoadScene("BattleScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
