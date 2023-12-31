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
                    Sequence sequence = DOTween.Sequence();
                    sequence.AppendInterval(2.5f);
                    sequence.AppendCallback(() => { ForceState(GameState.INTRODUCTION); });
                    break;
                }
            case GameState.INTRODUCTION:
                {
                    EnemyController.instance.ChangeDemonPosition();
                    Sequence sequence = DOTween.Sequence();
                    sequence.AppendInterval(2.5f);
                    sequence.AppendCallback(()=> { ForceState(GameState.START_GAME); });
                    
                    break;
                }
            case GameState.START_GAME:
                {
                    StartLevel();
                    PlayerController.instance.StartPlay();
                    EnemyController.instance.StartDemonPlay(); 
                    ObstacleController.instance.UpdateBombEnd();
                    break;
                }
            case GameState.NEXT_LEVEL:
                {
                    PlayerController.instance.OnPause();
                    EnemyController.instance.OnDemonPause();
                    ObstacleController.instance.OnBombPause();
                    ChangeState(GameState.INTRODUCTION);
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
    protected override void OnFixedUpdateState()
    {

    }

    void StartLevel()
    {
        level += 1;
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

    public bool IsBossLevel(){
        return level % 5 == 0;
    }
    // Update is called once per frame
    void Update()
    {
    }

}
