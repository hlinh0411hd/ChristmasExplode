using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

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
    public const int MAKE_LEVEL = 8;
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
        defaultState = DemonState.IDLE;
        ChangeState(defaultState);
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
                    OnSpawnEnemies();
                    break;
                }
            case DemonState.MAKE_LEVEL:
                {
                    MakeMagic();
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

    protected override void CheckLive()
    {
        if (health.health <= 0)
        {
            ForceState(DemonState.DIE);
        }
    }

    protected override void OnAttack()
    {
        animationMachine?.ChangeState(AnimationState.ATTACK, 1f);
        currentCountDownAttack = 1f;
        GetComponent<AudioSource>()?.Play();
    }
    protected void OnSpawnEnemies()
    {
        animationMachine?.ChangeState(AnimationState.SPAWN, 1f);
    }

    protected override void UpdateMove()
    {

    }

    void OnSteal()
    {
        crrWall = MapController.instance.GetWallDemon();
        UpdateNewPos();
    }

    void UpdateNewPos()
    {
        Vector2 pos = crrWall.transform.position;
        pos.y += 0.5f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(pos, 1f));
    }

    void MakeMagic()
    {
        // create item, enemy
        animationMachine?.ChangeState(AnimationState.SPELL, 1f);
    }
}
