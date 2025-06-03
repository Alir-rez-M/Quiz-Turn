using System;
using System.Collections;
using UnityEngine;

public class Player : BaseValues 
{
    
    
    AudioSource swordSoundEffect;
    [SerializeField] AudioClip swordClip;

    public event EventHandler OnAttackEnemy;
    float attack;
    public event EventHandler OnAttack;
    public event EventHandler OnHit;
    
    private void Start()
    {
        transform.position = startPosition.position;
        battleManager.OnChangedState += BattleManager_OnChangedState;
        swordSoundEffect = GetComponent<AudioSource>();
    }

    private void BattleManager_OnChangedState(object sender, BattleManager.OnChangedStateEventArgs e)
    {
        if (e.state == BattleManager.BattleState.Idle)
        {
            if (transform.position.x == startPosition.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if (e.state == BattleManager.BattleState.MoveToEnemy)
        {

            MoveToEnemy();


        }
        if (e.state == BattleManager.BattleState.Attack)
        {
            
            animator.SetBool("IsAttackin", true);
            StartCoroutine(SwordSoundEffect());

            



        }
        if (e.state == BattleManager.BattleState.BackToOriginalPositionPlayer)
        {
            
            StartCoroutine(BackToPosition());
            animator.SetBool("IsAttackin", false);
        }
        
        if(e.state == BattleManager.BattleState.ResultOfFight && PlayerHealth.Instance.GetCurrentHealth() > 0)
        {
            if (transform.position.x == startPosition.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
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
                    if (hit.transform.TryGetComponent(out EnemyHealth enemyHealth))
                    {
                        if (PlayerHealth.Instance.GetCurrentHealth() <= 2)
                        {
                            enemyHealth.Damage(damage * 1.5f);
                        }
                        else
                        {
                            enemyHealth.Damage(damage);
                        }

                    }
                }
            }

            if (attack > 0.8f)
            {
                

                OnAttack?.Invoke(this, EventArgs.Empty);
                
            }
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
    private IEnumerator SwordSoundEffect()
    {
        yield return new WaitForSeconds(0.2f);
        swordSoundEffect.PlayOneShot(swordClip);

    }
    public override void MoveToEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(goPoint.position.x, transform.position.y), Time.deltaTime * speed);
    }


}
