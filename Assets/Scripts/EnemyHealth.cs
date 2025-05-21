using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float health;
    public event EventHandler OnHit;
    bool canGetHit = true;


    public void Damage()
    {
        if (canGetHit)
        {
            canGetHit = false;
            health -= 1;
            Debug.Log(health);
            animator.SetTrigger("GotHit");
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
