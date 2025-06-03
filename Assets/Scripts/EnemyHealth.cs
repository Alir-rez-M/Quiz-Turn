using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour ,IUIManager
{
    public static EnemyHealth Instance { get; private set; }
    [SerializeField] private Animator animator;
    [SerializeField] private float health;
    public event EventHandler OnHit;
    bool canGetHit = true;
     float currentHealth;
    public event EventHandler<IUIManager.OnUIManagerEventArgs> OnUIManager;
    public event EventHandler OnFightEnd;
    private void Awake()
    {
        if (Instance != null)
        {
            Instance = null;
        }
        Instance = this;
    }
    private void Start()
    {
        currentHealth = health;
    }
    public void Damage(float damage)
    {
        if (canGetHit)
        {
            canGetHit = false;
            currentHealth -= damage;
            Debug.Log(currentHealth);
            OnUIManager?.Invoke(this, new IUIManager.OnUIManagerEventArgs()
            {
                enemyHealth = currentHealth / health,
            });
            animator.SetTrigger("GotHit");
            OnHit?.Invoke(this, EventArgs.Empty);
            StartCoroutine(CanGetHit());
            if (currentHealth <= 0)
            {
                OnFightEnd?.Invoke(this , EventArgs.Empty); 
            }
        }
    }
    public IEnumerator CanGetHit()
    {
        yield return new WaitForSeconds(0.5f);
        canGetHit = true;
    }
    public  float GetEnemyCurrentHealth()
    {
        return currentHealth;
    }
}
