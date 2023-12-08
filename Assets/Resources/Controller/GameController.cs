using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public const int SETUP_GAME = 1;
    public const int INTRODUCTION = 2;
    public const int START_GAME = 3;
    public const int NEXT_LEVEL = 4;
    public const int SAVE_GIFT = 5;
    public const int END_GAME = 6;
}

public class GameController : StateMachineObject
{
    public static GameController instance;

    public int level;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnSpawn();
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        defaultState = GameState.SETUP_GAME;
        ChangeState(defaultState);
    }

    protected override void OnChangeState()
    {
        switch (currentState)
        {
            case GameState.SETUP_GAME:
                {
                    ResetGame();
                    MapController.instance.SetUp();
                    PlayerController.instance.SetUp();
                    CameraControl.instance.SetUp();
                    EnemyController.instance.SetUp();
                    ForceState(GameState.INTRODUCTION);
                    break;
                }
            case GameState.INTRODUCTION:
                {
                    ObstacleController.instance.UpdateBombEnd();
                    ForceState(GameState.START_GAME);
                    break;
                }
            case GameState.START_GAME:
                {
                    PlayerController.instance.StartPlay();
                    break;
                }
            case GameState.NEXT_LEVEL:
                {
                    break;
                }
            case GameState.SAVE_GIFT:
                {
                    break;
                }
            case GameState.END_GAME:
                {
                    break;
                }
        }
    }


    protected override void OnUpdateState()
    {

    }
    public void ResetGame()
    {
        level = 0;
        isStart = false;
    }

    public void ResetData()
    {
    }

    public void OnDestroyAllEnemy()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

}
