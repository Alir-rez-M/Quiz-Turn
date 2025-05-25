using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] GameObject playerHealth;
    IUIManager manager;
    private void Start()
    {
        manager = playerHealth.GetComponent<IUIManager>();
        manager.OnUIManager += Manager_OnUIManager;
    }

    private void Manager_OnUIManager(object sender, IUIManager.OnUIManagerEventArgs e)
    {
        playerHealthBar.fillAmount = e.playerHealth ;
    }

    private void PlayerHealth_OnHealthBar(object sender, IUIManager.OnUIManagerEventArgs e)
    {
        
            
    }
}
