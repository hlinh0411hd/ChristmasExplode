using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyState
{
    public const int IDLE = 0;
    public const int MOVE = 1;
    public const int DASH = 2;
    public const int SKILL = 3;
    public const int ATTACK = 4;
    public const int DIE = 5;
}

public class Enemy : StateMachineObject
{
    public EnemyData dataControl;
    public EnemyData dataBase;
    public EnemyData data;


    protected Rigidbody2D rb;
    protected AnimationMachine animationMachine;
    protected bool isDead;
    protected Health health;

    protected AIEnemyAction aIEnemyAction;

    float currentCountDownAttack;

    float hor;
    float ver;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationMachine = GetComponent<AnimationMachine>();
        aIEnemyAction = GetComponent<AIEnemyAction>();
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void UpdateData(){
        data.maxHealth = dataBase.maxHealth * dataControl.maxHealth;
        data.damage = dataBase.damage * dataControl.damage;
        data.speed = dataBase.speed * dataControl.speed;
        data.shield = dataBase.shield * dataControl.shield;
        data.timeCountDownAttack = dataBase.timeCountDownAttack * dataControl.timeCountDownAttack;
        data.timeAttackDuration = dataBase.timeAttackDuration;
        data.rangeAttack = dataBase.rangeAttack;
    }
    private IEnumerator WaitStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isStart = true;

    }


    #region State
    public override void OnSpawn()
    {
        base.OnSpawn();
        UpdateData();
        isDead = false;
        currentCountDownAttack = 0;
        health.maxHealth = data.maxHealth;
        health.health = data.maxHealth;
        defaultState = EnemyState.IDLE;
        ChangeState(defaultState);
    }

    public override void OnStart()
    {
        aIEnemyAction.StartPlay();
        StartCoroutine(WaitStart(0.25f));
    }

    protected override void OnChangeState()
    {
        switch (currentState)
        {
            case EnemyState.IDLE:
                {
                    rb.velocity = Vector2.zero;
                    animationMachine?.ChangeState(AnimationState.IDLE);
                    break;
                }
            case EnemyState.MOVE:
                {
                    break;
                }
            case EnemyState.DASH:
                {
                    break;
                }
            case EnemyState.ATTACK:
                {
                    rb.velocity = Vector2.zero;
                    OnAttack();
                    break;
                }
            case EnemyState.SKILL:
                {
                    break;
                }
            case EnemyState.DIE:
                {
                    gameObject.SetActive(false);
                    Destroy(gameObject, 0.25f);
                    EnemyController.instance.OnDestroyEnemy();
                    break;
                }
        }
    }
    protected override void OnUpdateState()
    {
        switch (currentState)
        {
            case EnemyState.IDLE:
                {
                    break;
                }
            case EnemyState.MOVE:
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
        if (currentState == EnemyState.DIE)
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
            ForceState(EnemyState.DIE);
        }
    }

    protected virtual void OnAttack()
    {
        animationMachine?.ChangeState(AnimationState.ATTACK, data.timeAttackDuration);
        currentCountDownAttack = data.timeCountDownAttack;
        GetComponent<AudioSource>().Play();
    }

    protected virtual void UpdateMove()
    {
       
        //bool isSameTile = aIEnemyAction.GetTargetPos() == MapController.instance.ConvertPointToTile(transform.position);
        //bool isInRange = aIEnemyAction.GetTarget() == null ? true : Vector2.Distance(transform.position, MapController.instance.ConvertTileToPoint(aIEnemyAction.GetTargetPos())) <= aIEnemyAction.GetRangeAction(aIEnemyAction.GetTarget().tag);
        //if (!isSameTile || !isInRange){
        //    Vector2 pos = MapController.instance.ConvertTileToPoint(aIEnemyAction.GetTargetPos());
        //    Vector2 dif = pos - (Vector2)transform.position;
        //    if (dif.x < 0)
        //    {
        //        transform.localScale = new(1, 1);
        //    }
        //    else if (dif.x > 0)
        //    {
        //        transform.localScale = new(-1, 1);
        //    }
        //    rb.velocity = dif.normalized * data.speed;
        //}
        //else
        //{
        //    rb.velocity = Vector2.zero;
        //}
    }
}
