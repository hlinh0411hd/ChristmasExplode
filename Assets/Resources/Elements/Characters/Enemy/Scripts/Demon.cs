using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DemonState
{
    public const int IDLE = 0;
    public const int MOVE = 1;
    public const int DASH = 2;
    public const int SKILL = 3;
    public const int ATTACK = 4;
    public const int SPAWN = 5;
    public const int DIE = 6;
    public const int STEAL = 7;
}
public class Demon : Enemy
{
    GameObject crrWall;

    // Start is called before the first frame update
    void Start()
    {
    }



    #region State
    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    protected override void OnChangeState()
    {
        switch (currentState)
        {
            case DemonState.IDLE:
                {
                    rb.velocity = Vector2.zero;
                    animationMachine?.ChangeState(AnimationState.IDLE);
                    break;
                }
            case DemonState.MOVE:
                {
                    break;
                }
            case DemonState.DASH:
                {
                    break;
                }
            case DemonState.ATTACK:
                {
                    rb.velocity = Vector2.zero;
                    OnAttack();
                    break;
                }
            case DemonState.SKILL:
                {
                    break;
                }
            case DemonState.DIE:
                {
                    gameObject.SetActive(false);
                    Destroy(gameObject, 0.25f);
                    EnemyController.instance.OnDestroyEnemy();
                    break;
                }
                case DemonState.STEAL:
                {
                    OnSteal();
                    break;
                }
                case DemonState.SPAWN:
                {
                    break;
                }
        }
    }
    protected override void OnUpdateState()
    {
        switch (currentState)
        {
            case DemonState.IDLE:
                {
                    break;
                }
            case DemonState.MOVE:
                {
                    UpdateMove();
                    break;
                }
        }
        if (rb.velocity == Vector2.zero)
        {
            animationMachine?.ChangeState(AnimationState.IDLE);
        }
        else
        {
            animationMachine?.ChangeState(AnimationState.MOVE);
        }
    }
    protected override void OnFixedUpdateState()
    {
        switch (currentState)
        {
            case DemonState.IDLE:
                {
                    break;
                }
            case DemonState.MOVE:
                {
                    break;
                }
        }
    }
    #endregion State

    // Update is called once per frame
    protected new virtual void Update()
    {
        if (!isStart)
        {
            return;
        }
        base.Update();
        CheckLive();
        if (currentState == DemonState.DIE)
        {
            return;
        }
        UpdateTime();
        OnUpdateState();
    }

    void UpdateTime()
    {
        if (currentCountDownAttack >= 0)
        {
            currentCountDownAttack -= Time.deltaTime;
        }
    }

    protected virtual void CheckLive()
    {
        if (health.health <= 0)
        {
            ForceState(DemonState.DIE);
        }
    }

    protected virtual void OnAttack()
    {
        animationMachine?.ChangeState(AnimationState.ATTACK, 1f);
        currentCountDownAttack = 1f;
        GetComponent<AudioSource>().Play();
    }

    protected virtual void UpdateMove()
    {
       
    }

    void OnSteal(){
        crrWall = MapController.instance.GetWallDemon();
        UpdateNewPos();
        MakeMagic();
    }

    void UpdateNewPos(){
    }
    
    void MakeMagic(){
        // create item, enemy
    }
}
