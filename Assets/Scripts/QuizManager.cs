
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private GameObject questions;
    [SerializeField] private BattleManager battle;



    private void Start()
    {
        battle.OnChangedState += Battle_OnChangedState;
        questions.SetActive(false);
    }

    private void Battle_OnChangedState(object sender, BattleManager.OnChangedStateEventArgs e)
    {
        if (e.state == BattleManager.BattleState.Idle)
        {
            questions.SetActive(true);
        }
        else
        {
            questions.SetActive(false);
        }
    }
}
