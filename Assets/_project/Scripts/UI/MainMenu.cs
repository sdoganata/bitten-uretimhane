using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public Button resumeButton;
    public TextMeshProUGUI startButtonTMP;
    public GameDirector gameDirector;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, .2f).SetUpdate(true);

    }
    public void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }


    public void StartButtonPressed()
    {
        Time.timeScale = 1;
        gameDirector.RestartLevel();
        Hide();
    }

    public void ResumeButtonPressed()
    {
        Time.timeScale = 1;
        gameDirector.gameState = GameState.GamePlay;
        gameDirector.ShowInGameUI();
        Hide();
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    internal void EnableResumeButton()
    {
        resumeButton.interactable = true;
    }
}
