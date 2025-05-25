using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerTextBattle;
    [SerializeField] GameObject battleManager;
    IUIManager uiManager;

    private void Start()
    {
        uiManager = battleManager.GetComponent<IUIManager>();
        uiManager.OnUIManager += UiManager_OnUIManager;
    }

    private void UiManager_OnUIManager(object sender, IUIManager.OnUIManagerEventArgs e)
    {
        timerImage.fillAmount = e.uiBar;
        timerText.text = e.timer.ToString();
        timerTextBattle.text = e.battleTimer.ToString();
    }
}
