using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BombState
{
    public const int WAIT = 0;
    public const int RUN = 1;
    public const int EXPLODE = 2;
}

public class Bomb : StateMachineObject
{
    public float timeCountDown;
    bool isDead;

    private IEnumerator WaitStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isStart = true;
        ChangeState(BombState.RUN);
    }


    #region State
    public override void OnSpawn()
    {
        base.OnSpawn();
        isDead = false;
        defaultState = BombState.WAIT;
        ChangeState(defaultState);
    }

    public override void OnStart()
    {
        StartCoroutine(WaitStart(0.25f));
    }

    protected override void OnChangeState()
    {
        switch (currentState)
        {
            case BombState.WAIT:
                {
                    isStart = false;
                    break;
                }
            case BombState.RUN:
                {
                    break;
                }
            case BombState.EXPLODE:
                {
                    OnExplode();
                    break;
                }
        }
    }
    protected override void OnUpdateState()
    {
        switch (currentState)
        {
            case BombState.WAIT:
                {
                    break;
                }
            case BombState.RUN:
                {
                    if (timeCountDown < 0){
                        ChangeState(BombState.EXPLODE);
                    }
                    break;
                }
            case BombState.EXPLODE:
                {
                    break;
                }
        }
    }
    protected override void OnFixedUpdateState()
    {
    }
    #endregion State

    // Update is called once per frame
    protected new virtual void Update()
    {
        if (!isStart || isDead)
        {
            return;
        }
        base.Update();
        UpdateTime();
        OnUpdateState();
    }

    void UpdateTime()
    {
        if (timeCountDown >= 0){
            timeCountDown -= Time.deltaTime;
        }
    }

    public void ResetTime(){
        timeCountDown = 60;
    }

    public void OnExplode(){
        isDead = true;
        GameController.instance.ChangeState(GameState.END_GAME);
    }

}