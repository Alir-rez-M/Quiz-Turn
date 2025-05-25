using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour , IUIManager
{
    [SerializeField] private Animator animator;
    [SerializeField] private float health;
    float currentHealth;
    public event EventHandler OnHit;
    public event EventHandler<IUIManager.OnUIManagerEventArgs> OnUIManager;
    
    bool canGetHit = true;
    private void Start()
    {
        currentHealth = health;
    }

    private void Update()
    {
        
    }
    public void Damage()
    {

        
        if (canGetHit)
        {
            canGetHit = false;
            health -= 1;
            Debug.Log(health);
            animator.SetTrigger("GotHit");
            OnUIManager?.Invoke(this , new IUIManager.OnUIManagerEventArgs()
            {
                playerHealth = health /currentHealth,
            });
          
            
            OnHit?.Invoke(this, EventArgs.Empty);
            StartCoroutine(CanGetHit());
            
        }
        
    }
    public IEnumerator CanGetHit()
    {
        yield return new WaitForSeconds(0.5f);
        canGetHit = true;
    }

}
