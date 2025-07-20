using System;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;

    [Header("Manager")]
    public LevelManager levelManager;
    public CoinManager coinManager;
    public FXManager fxManager;
    public AudioManager audioManager;
    public Player player;
    //public ParticleSystem testPS;

    [Header("UI")]
    public MainMenu mainMenu;
    public PlayerHealthUI playerHealthUI;
    public PlayerHitUI playerHitUI;
    public MessageUI messageUI;
    public InventoryUI inventoryUI;

    public CameraHolder cameraHolder;


    public GameState gameState;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameState = GameState.MainMenu;
        HideInGameUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            mainMenu.Hide();
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadPreviousLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            gameState = GameState.MainMenu;
            mainMenu.Show();
            mainMenu.EnableResumeButton();
            mainMenu.startButtonTMP.text = "RESTART";
            HideInGameUI();
        }
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            testPS.Play();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            testPS.Stop();
        }*/
    }

    public void RestartLevel()
    {
        gameState = GameState.GamePlay;
        levelManager.RestartLevelManager();
        //coinManager.RestartCoinManager();
        player.RestartPlayer();
        ShowInGameUI();
    }


    void LoadNextLevel()
    {
        if (levelManager.levelNo < levelManager.levels.Count)
        {
            levelManager.levelNo += 1;
        }
        RestartLevel();
    }

    void LoadPreviousLevel()
    {
        if (levelManager.levelNo > 1)
        {
            levelManager.levelNo -= 1;
        }
        RestartLevel();
    }

    public void LevelCompleted()
    {
        Invoke(nameof(LoadNextLevel), 1f);
    }
    public void Lose()
    {

    }

    internal void ShowInGameUI()
    {
        playerHealthUI.Show();
        coinManager.coinUI.Show();
        inventoryUI.Show();
    }

    internal void HideInGameUI()
    {
        playerHealthUI.Hide();
        coinManager.coinUI.Hide();
        inventoryUI.Hide();
    }
}


public enum GameState
{
    MainMenu,
    GamePlay,
    VictoryUI,
    FailUI,
}