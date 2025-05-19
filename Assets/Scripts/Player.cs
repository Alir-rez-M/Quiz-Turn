using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private Transform goPoint;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float speed;
    [SerializeField] private float attackDuration;
    [SerializeField] private Transform attackingPoint;
    [SerializeField] private float radius;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask enemy;
    private float attackTimer;

    public event EventHandler OnAttackEnemy;
    Vector2 velocity;
    float attack;
    public event EventHandler OnAttack;
    private void Start()
    {
        transform.position = startPosition.position;
        battleManager.OnChangedState += BattleManager_OnChangedState;
    }

    private void BattleManager_OnChangedState(object sender, BattleManager.OnChangedStateEventArgs e)
    {
        
        if (e.state == BattleManager.BattleState.MoveToEnemy)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(goPoint.position.x , transform.position.y), Time.deltaTime * speed);
            StopAllCoroutines();    
            
        }
        if (e.state == BattleManager.BattleState.Attack)
        {
            animator.SetBool("IsAttackin", true);
            
            
        }
        if (e.state == BattleManager.BattleState.BackToOriginalPositionPlayer)
        {
            
            StartCoroutine(BackToPosition());
            animator.SetBool("IsAttackin", false);
        }
        if (e.state == BattleManager.BattleState.Idle)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
        
    }
    private void Update()
    {
        if (transform.position.x == goPoint.position.x)
        {
            OnAttackEnemy?.Invoke(this , EventArgs.Empty);  
            attack += Time.deltaTime;
            if (attack > 0.4 && attack < 0.6)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(attackingPoint.position, radius, enemy);
                foreach (Collider2D hit in hits)
                {
                    if (hit.transform.TryGetComponent(out Enemy enemy))
                    {
                        enemy.Damaged();
                    }
                }
            }

            if (attack > 0.8f)
            {
                

                OnAttack?.Invoke(this, EventArgs.Empty);
                
            }
            velocity = new Vector2(-10, 10);
        }
        if (transform.position.x == goPoint.position.x || transform.position.x == startPosition.position.x)
        {
            animator.SetBool("IsMoving" , false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
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
