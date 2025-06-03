
using UnityEngine;
using UnityEngine.UI;

public class HUDUIManager : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] GameObject playerHealth;
    [SerializeField] GameObject enemyHealth;
    IUIManager playerHealthmanager;
    IUIManager enemyHealthmanager;
    private void Start()
    {
        playerHealthmanager = playerHealth.GetComponent<IUIManager>();
        enemyHealthmanager = enemyHealth.GetComponent<IUIManager>();
        playerHealthmanager.OnUIManager += Manager_OnUIManager;
        enemyHealthmanager.OnUIManager += EnemyHealthmanager_OnUIManager;
    }

    private void EnemyHealthmanager_OnUIManager(object sender, IUIManager.OnUIManagerEventArgs e)
    {
        enemyHealthBar.fillAmount = e.enemyHealth;
    }

    private void Manager_OnUIManager(object sender, IUIManager.OnUIManagerEventArgs e)
    {
        playerHealthBar.fillAmount = e.playerHealth ;
    }

    private void PlayerHealth_OnHealthBar(object sender, IUIManager.OnUIManagerEventArgs e)
    {
        
            
    }
}
