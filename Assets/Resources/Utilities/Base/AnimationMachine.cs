using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState{
    public const string IDLE = "idle";
    public const string MOVE = "move";
    public const string ATTACK = "attack";
    public const string SKILL = "skill";
    public const string HURT = "hurt";
    public const string DIE = "die";
}

public class AnimationMachine: MonoBehaviour
{
    public Animator animator;
    protected string currentState;
    protected string defaultState;

    protected float timeState;

    public virtual void OnSpawn()
    {
        defaultState = AnimationState.IDLE;
    }
    public virtual void OnStart()
    {
        ChangeState(defaultState);
    }

    public void ChangeState(string state, float time = -1f)
    {
        if (timeState > 0)
        {
            return;
        }
        if (currentState != state)
        {
            currentState = state;
            OnChangeState();
            timeState = time;
        }
    }

    public void ForceState(string state, float time = -1f)
    {
        timeState = time;
        currentState = state;
        OnChangeState();
    }

    protected void Update()
    {
        UpdateTimeState();
    }

    void UpdateTimeState()
    {
        if (timeState >= 0)
        {
            timeState -= Time.deltaTime;
            if (timeState < 0)
            {
                ChangeState(defaultState, -1f);
            }
        }
    }

    public string GetState()
    {
        return currentState;
    }

    protected void OnChangeState(){
        animator?.Play(currentState);
    }

}