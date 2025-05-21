using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float health;
    public event EventHandler OnHit;
    bool canGetHit = true;

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
            StartCoroutine(CanGetHit());
        }
    }
    public IEnumerator CanGetHit()
    {
        OnHit?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(0.5f);
        canGetHit = true;
    }

}
