using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour , IUIManager
{
    public static PlayerHealth Instance {  get; private set; }  
    [SerializeField] private Animator animator;
    [SerializeField] private float health;
    [SerializeField] private GameObject deadVisual;
    [SerializeField] private GameObject alive;
    [SerializeField] private Transform deadPosition;
    float currentHealth;
    public event EventHandler OnHit;
    public event EventHandler<IUIManager.OnUIManagerEventArgs> OnUIManager;
    public event EventHandler OnFightEnd;
    bool canGetHit = true;
    private void Start()
    {
        currentHealth = health;
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Instance = null;
        }
        Instance = this;
    }

    public void Damage(float damage)
    {

        
        if (canGetHit)
        {
            canGetHit = false;
            currentHealth -= damage ;
            Debug.Log(health);
            animator.SetTrigger("GotHit");
            OnUIManager?.Invoke(this , new IUIManager.OnUIManagerEventArgs()
            {
                playerHealth = currentHealth /health,
            });
          
            
            OnHit?.Invoke(this, EventArgs.Empty);
            StartCoroutine(CanGetHit());
            if (currentHealth <= 0)
            {
                OnFightEnd?.Invoke(this, EventArgs.Empty);
                Instantiate(deadVisual, deadPosition);
                alive.SetActive(false);
            }

        }
        
    }
    public IEnumerator CanGetHit()
    {
        yield return new WaitForSeconds(0.5f);
        canGetHit = true;
    }
    public  float GetCurrentHealth()
    {
        return currentHealth;
    }

}
