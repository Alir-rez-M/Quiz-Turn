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
            Debug.Log("Attack");
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
            if (attack > 0.8f)
            {
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
        }

    }
    private IEnumerator BackToPosition()
    {
        yield return new WaitForSeconds(attackDuration);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        transform.position = Vector2.MoveTowards(transform.position , new Vector2(startPosition.position.x , transform.position.y) , speed * Time.deltaTime);

    }
    public void Damaged()
    {
        Debug.Log("Dameged!!!");
    }
}
