using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerState
{
    public const int IDLE = 0;
    public const int MOVE = 1;
    public const int JUMP = 2;
    public const int SKILL = 3;
    public const int ATTACK = 4;
    public const int DIE = 5;
}
public class Player : StateMachineObject, IControlable
{
    public PlayerData data;
    public float dir;

    protected Rigidbody2D rb;
    protected AnimationMachine animationMachine;

    protected Health health;

    protected float hor;
    protected float ver;

    protected int numJump;


    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        animationMachine = GetComponent<AnimationMachine>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        InitInfo();
    }

    public void InitInfo()
    {
        dir = 1;
        numJump = 0;
    }

    // Update is called once per frame
    new public void Update()
    {
        if (!isStart)
        {
            return;
        }
        base.Update();
        OnUpdateState();
        OnPressedMove();
        OnPressedFire();
        OnKeyPressed();
    }
    #region State
    public override void OnSpawn()
    {
        base.OnSpawn();
        health.maxHealth = health.health = data.maxHealth;
        defaultState = PlayerState.IDLE;
        ChangeState(defaultState);
        isStart = false;
    }

    public override void OnStart()
    {
        StartCoroutine(WaitStart(0.25f));
    }
    private IEnumerator WaitStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isStart = true;

    }

    protected override void OnChangeState()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                {
                    rb.velocity = Vector2.zero;
                    animationMachine?.ChangeState(AnimationState.IDLE, -1f);
                    break;
                }
            case PlayerState.MOVE:
                {
                    break;
                }
            case PlayerState.JUMP:
                {
                    OnJump();
                    break;
                }
            case PlayerState.ATTACK:
                {
                    OnAttack();
                    break;
                }
            case PlayerState.SKILL:
                {
                    break;
                }
            case PlayerState.DIE:
                {
                    gameObject.SetActive(false);
                    break;
                }
        }
    }

    protected virtual void OnAttack(){
    }
    protected override void OnUpdateState()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                {
                    break;
                }
            case PlayerState.MOVE:
                {
                    OnMove();
                    break;
                }
        }
        if (rb.velocity == Vector2.zero)
        {
            animationMachine?.ChangeState(AnimationState.IDLE, -1f);
        }
        else
        {
            animationMachine?.ChangeState(AnimationState.MOVE, -1f);
        }
    }
    #endregion State

    protected void OnMove()
    {
        if (hor < 0)
        {
            transform.localScale = new(1, 1);
        }
        else if (hor > 0)
        {
            transform.localScale = new(-1, 1);
        }
        rb.velocity = new Vector2(hor, ver) * data.speed;
    }
    

    protected void OnJump(){
        rb.velocity += new Vector2(0, data.speed);
        numJump += 1;
    }

    #region Control
    public virtual void OnPressedMove()
    {
        hor = Input.GetAxisRaw("Horizontal");
        if (hor == 0)
        {
            ChangeState(PlayerState.IDLE);
        }
        else
        {
            ChangeState(PlayerState.MOVE);
        }
    }
    public void OnPressedFire()
    {
        if (Input.GetKeyDown("q"))
        {
            ChangeState(PlayerState.ATTACK);
        }
        if (Input.GetKeyDown("space")){
            if (numJump < 1){
                ChangeState(PlayerState.JUMP);
            }
        }
    }
    public void OnKeyPressed()
    {
    }
    #endregion Control

    public List<string> GetListTagEnemy(){
        List<string> list = new List<string>();
        list.Add(GameConfig.TAG_ENEMY);
        return list;
    }
}
