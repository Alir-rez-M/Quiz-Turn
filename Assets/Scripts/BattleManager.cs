using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour , IUIManager
{
    [SerializeField] private List<AnswerButton> answerButtons;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private QuestionSetup questionSetup;
    [SerializeField] private float quizDuration;
    [SerializeField] private float battleDuration;
    public BattleState state;
    public event EventHandler<OnChangedStateEventArgs> OnChangedState;
    private float battleStart;
    float attackDuration;
    float quizTimer;
    float battleTimer;
    int test = 10;
    public event EventHandler<IUIManager.OnUIManagerEventArgs> OnUIManager;
    

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
        BackToOriginalPositionEnemy,
        ResultOfFight

    }

    private void Start()
    {

        quizTimer = quizDuration;
        battleTimer = battleDuration;
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
        EnemyHealth.Instance.OnFightEnd += Instance_OnFightEnd;
        PlayerHealth.Instance.OnFightEnd += Instance_OnFightEnd1;
        
    }

    private void Instance_OnFightEnd1(object sender, EventArgs e)
    {
        state = BattleState.ResultOfFight;
    }

    private void Instance_OnFightEnd(object sender, EventArgs e)
    {
        state = BattleState.ResultOfFight;
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

    private void Update()
    {
        battleTimer -= Time.deltaTime;
        OnUIManager?.Invoke(this, new IUIManager.OnUIManagerEventArgs
        {
                battleTimer = BattleTimerFunction(battleTimer)
        });
        if (battleTimer <= 0)
        {
            state = BattleState.ResultOfFight;
        }
        

        switch (state)
        {
            case BattleState.Idle:
                battleStart = 0;
                quizTimer -= Time.deltaTime;
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                if (quizTimer <= 0)
                {
                    quizTimer = 0;
                    state = BattleState.MoveToPlayer;
                }
                

                OnUIManager?.Invoke(this, new IUIManager.OnUIManagerEventArgs
                {
                    uiBar = quizTimer / quizDuration,
                    timer = TimerFunction(quizTimer),
                    battleTimer = BattleTimerFunction(battleTimer)
                });
                
                


                break;
            case BattleState.QuizStart:

                
                break;
            case BattleState.MoveToPlayer:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                quizTimer = quizDuration;
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
                quizTimer = quizDuration;
                break;
            case BattleState.BackToOriginalPositionEnemy:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                battleStart += Time.deltaTime;
                if (battleStart > 1.2f)
                {
                    if (questionSetup.questions.Count > 0 && PlayerHealth.Instance.GetCurrentHealth() > 0 && EnemyHealth.Instance.GetEnemyCurrentHealth() > 0)
                    {
                        questionSetup.Start();

                        state = BattleState.Idle;
                    }
                    else
                    {
                        state = BattleState.ResultOfFight;
                    }
                }
                break;
            case BattleState.BackToOriginalPositionPlayer:
                OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                {
                    state = state,
                });
                battleStart += Time.deltaTime;
                if (battleStart > 1.2f)
                {
                    if(questionSetup.questions.Count > 0 && PlayerHealth.Instance.GetCurrentHealth() > 0 && EnemyHealth.Instance.GetEnemyCurrentHealth() > 0 )
                    {
                        questionSetup.Start();

                        state = BattleState.Idle;
                    }
                    else
                    {
                        state = BattleState.ResultOfFight;
                    }
                }
                break;
            case BattleState.ResultOfFight:
                battleDuration = 0;
                battleTimer = 0;    
                if(PlayerHealth.Instance.GetCurrentHealth() > EnemyHealth.Instance.GetEnemyCurrentHealth())
                {
                    Debug.Log("Player Wins");
                    OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                    {
                        state = state,
                    });
                    FightsResult.Instance.PlayerWon();
                }
                if (PlayerHealth.Instance.GetCurrentHealth() < EnemyHealth.Instance.GetEnemyCurrentHealth())
                {
                    Debug.Log("Player Wins");
                    OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                    {
                        state = state,
                    });
                    FightsResult.Instance.EnemyWon();
                }
                if(PlayerHealth.Instance.GetCurrentHealth() == EnemyHealth.Instance.GetEnemyCurrentHealth())
                {
                    OnChangedState?.Invoke(this, new OnChangedStateEventArgs
                    {
                        state = state,
                    });
                    FightsResult.Instance.Draw();
                }
                break;
        }
    }

    private void Button_OnAnswer(object sender, System.EventArgs e)
    {
        state = BattleState.MoveToEnemy;
    }
    

    public float TimerFunction(float curentTime)
    {
        curentTime += 1;

        float sec = Mathf.FloorToInt(curentTime % 60);
        return sec;
    }
    public float BattleTimerFunction(float curentTime)
    {
        curentTime += 1;

        float sec = Mathf.FloorToInt(curentTime);
        return sec;
    }

}
