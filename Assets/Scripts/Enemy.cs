using System;
using System.Collections;
using UnityEngine;

public class Enemy : BaseValues
{
    AudioSource swordSoundEffect;
    [SerializeField] AudioClip swordClip;
    private float attack;
    public event EventHandler OnEnemyAttack;
    public event EventHandler OnStartAttack;
    private void Start()
    {
        transform.position = startPosition.position;
        battleManager.OnChangedState += BattleManager_OnChangedState;
        swordSoundEffect = GetComponent<AudioSource>();
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

            StartCoroutine(SwordSoundEffect());

        }
        if (e.state == BattleManager.BattleState.BackToOriginalPositionEnemy)
        {
            StartCoroutine(BackToPosition());
        }
        if (e.state == BattleManager.BattleState.Idle)
        {

            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (e.state == BattleManager.BattleState.ResultOfFight && EnemyHealth.Instance.GetEnemyCurrentHealth() > 0)
        {
            if (transform.position.x == startPosition.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
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
                Collider2D[] hits = Physics2D.OverlapCircleAll(attackingPoint.position, radius);
                foreach (Collider2D hit in hits)
                {
                    if (hit.transform.TryGetComponent(out PlayerHealth playerHealth))
                    {
                        playerHealth.Damage(damage);
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
    private IEnumerator SwordSoundEffect()
    {
        yield return new WaitForSeconds(0.2f);
        swordSoundEffect.PlayOneShot(swordClip);

    }
}
