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
    public GroundCheck groundCheck;
    public float dir;

    public float jumpHoldTime = 0.5f;
    public float cancelRate = 100;

    protected Rigidbody2D rb;
    protected AnimationMachine animationMachine;

    protected Health health;

    protected float hor;
    protected float ver;

    [SerializeField] protected int numJump;
    [SerializeField] protected bool jumping;
    [SerializeField] protected bool jumpCancel;
    [SerializeField] protected float jumpTime;


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
        dir = -1;
        numJump = 0;
        hor = ver = 0;
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
        OnKeyReleased();
    }

    protected new void FixedUpdate()
    {
        if (!isStart)
        {
            return;
        }
        base.FixedUpdate();
        OnFixedUpdateState();
    }
    #region State
    public override void OnSpawn()
    {
        base.OnSpawn();
        health.maxHealth = health.health = data.maxHealth;
        defaultState = PlayerState.IDLE;
        ChangeState(defaultState);
        animationMachine?.ChangeState(AnimationState.IDLE);
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

    protected virtual void OnAttack()
    {
        animationMachine?.ForceState(AnimationState.ATTACK, 0.5f);
    }
    protected override void OnFixedUpdateState()
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
            case PlayerState.JUMP:
                {

                    break;
                }
        }

        if (jumping && jumpCancel && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelRate);
            animationMachine?.ForceState(AnimationState.JUMP_DOWN, 1f);
        }
    }
    protected override void OnUpdateState()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                {
                    animationMachine?.ChangeState(AnimationState.IDLE);
                    break;
                }
            case PlayerState.MOVE:
                {
                    animationMachine?.ChangeState(AnimationState.MOVE);
                    break;
                }
        }

        if (hor < 0)
        {
            transform.localScale = new(1 * dir, 1);
        }
        else if (hor > 0)
        {
            transform.localScale = new(-1 * dir, 1);
        }
        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (jumpTime > jumpHoldTime)
            {
                jumping = false;
            }
        }
        if (rb.velocity.y < 0)
        {
            animationMachine?.ChangeState(AnimationState.JUMP_DOWN, 0.5f);
        }
        if (rb.velocity == Vector2.zero)
        {
            ChangeState(PlayerState.IDLE);
        }
    }
    #endregion State

    protected void OnMove()
    {
        transform.Translate(new Vector2(hor * data.speed * Time.deltaTime, 0));
    }


    protected void OnJump()
    {
        float jumpForce = Mathf.Sqrt(data.heightJump * -2 * (Physics2D.gravity.y * rb.gravityScale));
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        numJump += 1;
        jumping = true;
        jumpCancel = false;
        jumpTime = 0;
        animationMachine?.ForceState(AnimationState.JUMP_UP, 1f);
    }

    #region Control
    public virtual void OnPressedMove()
    {
        hor = Input.GetAxisRaw("Horizontal");
        if (hor != 0)
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
    }
    public void OnKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (numJump < 1 && groundCheck.isGrounded)
            {
                ChangeState(PlayerState.JUMP);
            }
        }
    }

    public void OnKeyReleased()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (jumping)
            {
                jumpCancel = true;
            }
        }
    }
    #endregion Control

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameConfig.TAG_WALL)
        {
            if (groundCheck.isGrounded && transform.position.y >= collision.transform.position.y - 0.1f)
            {
                numJump = 0;
                jumping = false;
                jumpCancel = false;
                ForceState(PlayerState.IDLE);
                animationMachine?.ForceState(AnimationState.IDLE);
            }
        }
    }
}
