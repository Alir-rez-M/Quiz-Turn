// This script is for the buttons the answers will go on

using UnityEngine;
using TMPro;
using System;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField] private TextMeshProUGUI answerText;
    public event EventHandler OnCorrectAnswer;
    public event EventHandler OnWrongtAnswer;

    // To make it ask a new question after the first question
    [SerializeField] private QuestionSetup questionSetup;

    public void SetAnswerText(string newText)
    {
        answerText.text = newText;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if(isCorrect)
        {
            Debug.Log("CORRECT ANSWER");
            OnCorrectAnswer?.Invoke(this , EventArgs.Empty);   
        }
        else
        {
            OnWrongtAnswer?.Invoke(this , EventArgs.Empty);
            Debug.Log("WRONG ANSWER");
        }

        
    }
}
