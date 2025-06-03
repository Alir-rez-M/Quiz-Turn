using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightsResult : MonoBehaviour
{
    public static FightsResult Instance { private set; get; }
    [SerializeField] private GameObject playerWon;
    [SerializeField] private GameObject enemtWon;
    [SerializeField] private GameObject draw;

    private void Awake()
    {
        if (Instance != null)
        {
            Instance = null;
        } Instance = this;  
    }
    public void PlayerWon()
    {
        playerWon.SetActive(true);
    }
    public void EnemyWon()
    {
        enemtWon.SetActive(true);
    }
    public void Draw()
    {
        draw.SetActive(true);
    }
}


