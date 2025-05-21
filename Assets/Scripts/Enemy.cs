using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private Transform goPoint;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float speed;
    [SerializeField] float attackDuration;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackingPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask player;
    private float attackTimer;
    private float attack;
    Vector2 velocity;
    public event EventHandler OnEnemyAttack;
    public event EventHandler OnStartAttack;
    private void Start()
    {
        transform.position = startPosition.position;
        battleManager.OnChangedState += BattleManager_OnChangedState;
    }

    private void BattleManager_OnChangedState(object sender, BattleManager.OnChangedStateEventArgs e)
    {
        if (e.state == BattleManager.BattleState.MoveToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(goPoint.position.x, transform.position.y), Time.deltaTime * speed);
            StopAllCoroutines();

        }
        if (e.state == BattleManager.BattleState.EnemyAttack)
        {
            animator.SetBool("IsAttacking" , true);
        }
        if (e.state == BattleManager.BattleState.BackToOriginalPositionEnemy)
        {
            StartCoroutine(BackToPosition());
        }
        if (e.state == BattleManager.BattleState.Idle)
        {

            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void LateUpdate()
    {
        if (transform.position.x == goPoint.position.x)
        {
            OnStartAttack?.Invoke(this , EventArgs.Empty);
            attack += Time.deltaTime;
            if (attack > 0.4f && attack < 0.6f)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(attackingPoint.position, radius, player);
                foreach (Collider2D hit in hits)
                {
                    if (hit.transform.TryGetComponent(out PlayerHealth playerHealth))
                    {
                        playerHealth.Damage();
                    }
                }
            }
            if (attack > 0.8f)
            {
                animator.SetBool("IsAttacking", false);
                OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            }

            
        }
        if (transform.position.x == startPosition.position.x || transform.position.x == goPoint.position.x)
        {
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsRunning", true);
            attack = 0;
        }

    }
    private IEnumerator BackToPosition()
    {
        yield return new WaitForSeconds(attackDuration);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        transform.position = Vector2.MoveTowards(transform.position , new Vector2(startPosition.position.x , transform.position.y) , speed * Time.deltaTime);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(attackingPoint.position, radius);
    }
    public void Damaged()
    {
        Debug.Log("Dameged!!!");
    }
}
