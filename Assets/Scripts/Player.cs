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
    [SerializeField] float attackDuration;
    [SerializeField] Animator animator;
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

            
        }
        if (e.state == BattleManager.BattleState.BackToOriginalPositionPlayer)
        {
            
            StartCoroutine(BackToPosition());
        }
        
    }
    private void Update()
    {
        if (transform.position.x == goPoint.position.x)
        {
            OnAttackEnemy?.Invoke(this , EventArgs.Empty);  
            attack += Time.deltaTime;

            if (attack > 2)
            {

                OnAttack?.Invoke(this, EventArgs.Empty);
                
            }
            velocity = new Vector2(-10, 10);
        }
        if (transform.position.x == goPoint.position.x || transform.position.x == startPosition.position.x)
        {
            animator.SetBool("IsMoving" , false);

            if (transform.position.x == startPosition.position.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
        
    }
    private IEnumerator BackToPosition()
    {
        yield return new WaitForSeconds(attackDuration);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        transform.position = Vector2.MoveTowards(transform.position , new Vector2(startPosition.position.x , transform.position.y) , speed * Time.deltaTime);
        

    }

}
