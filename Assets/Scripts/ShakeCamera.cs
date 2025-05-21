using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealth.OnHit += Player_OnHit;
    }

    private void Player_OnHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger("IsShaking");
    }

}
