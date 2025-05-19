using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<AnswerButton> answerButtons;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private QuestionSetup questionSetup;
    public BattleState state;
    public event EventHandler<OnChangedStateEventArgs> OnChangedState;
    private float battleStart;
    float attackDuration;
    public class OnChangedStateEventArgs
    {
        public BattleState state;
    }
    public enum BattleState
    {
        Idle,
        QuizStart,
        MoveToPlayer,
        MoveToEnemy,
        Attack,
        EnemyAttack,
        BackToOriginalPositionPlayer,
        BackToOriginalPositionEnemy

    }

    private void Start()
    {
        state = BattleState.Idle;
        foreach (var button in answerButtons)
        {
            button.OnCorrectAnswer += Button_OnAnswer;
            button.OnWrongtAnswer += Button_OnWrongtAnswer;
        }
        player.OnAttack += Player_OnAttack;
        player.OnAttackEnemy += Player_OnAttackEnemy;
        enemy.OnEnemyAttack += Enemy_OnEnemyAttack;
        enemy.OnStartAttack += Enemy_OnStartAttack;
        
    }

    private void Enemy_OnStartAttack(object sender, EventArgs e)
    {
        state = BattleState.EnemyAttack;
    }

    private void Player_OnAttackEnemy(object sender, EventArgs e)
    {
        state = BattleState.Attack;
    }

    private void Enemy_OnEnemyAttack(object sender, EventArgs e)
    {
        state = BattleState.BackToOriginalPositionEnemy;
    }

    private void Button_OnWrongtAnswer(object sender, EventArgs e)
    {
        state = BattleState.MoveToPlayer;
    }

    private void Player_OnAttack(object sender, EventArgs e)
    {
        state = BattleState.BackToOriginalPositionPlayer;
    }

    private void LateUpdate()
    {
        switch (state)
        {
            case BattleState.Idle:
                battleStart = 0;
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                break;
            case BattleState.QuizStart:

                
                break;
            case BattleState.MoveToPlayer:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                break;
            case BattleState.Attack:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                break;
            case BattleState.EnemyAttack:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                break;
            case BattleState.MoveToEnemy:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                break;
            case BattleState.BackToOriginalPositionEnemy:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                battleStart += Time.deltaTime;
                if (battleStart > 1.5)
                {
                    questionSetup.Start();
                    state = BattleState.Idle;
                }
                break;
            case BattleState.BackToOriginalPositionPlayer:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                battleStart += Time.deltaTime;
                if (battleStart > 1.5)
                {
                    questionSetup.Start();
                    state = BattleState.Idle;
                }
                break;
        }
    }

    private void Button_OnAnswer(object sender, System.EventArgs e)
    {
        state = BattleState.MoveToEnemy;
    }
}
