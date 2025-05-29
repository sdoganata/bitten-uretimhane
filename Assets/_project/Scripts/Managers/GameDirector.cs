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
    public CameraHolder cameraHolder;
    //public ParticleSystem testPS;

    [Header("UI")]
    public PlayerHeatlhUI playerHeatlhUI;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RestartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
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
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            testPS.Play();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            testPS.Stop();
        }*/
    }

    private void RestartLevel()
    {
        levelManager.RestartLevelManager();
        //coinManager.RestartCoinManager();
        player.RestartPlayer();
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
}
