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
    private float attackTimer;
    Vector2 velocity;
    public event EventHandler OnEnemyAttack;
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
        if (e.state == BattleManager.BattleState.BackToOriginalPositionEnemy)
        {
            StartCoroutine(BackToPosition());
        }
    }
    private void LateUpdate()
    {
        if (transform.position.x == goPoint.position.x)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            velocity = new Vector2(10, 10);
        }

    }
    private IEnumerator BackToPosition()
    {
        yield return new WaitForSeconds(attackDuration);
        transform.position = Vector2.SmoothDamp(transform.position, startPosition.position, ref velocity, 0.7f);

    }
    public void Damaged()
    {
        Debug.Log("Dameged!!!");
    }
}
