using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineObject: MonoBehaviour
{
    [SerializeField]
    protected int currentState = -1;
    protected int defaultState;

    protected float timeState;

    protected bool isStart;

    public virtual void OnSpawn()
    {
        isStart = false;
    }
    public virtual void OnStart()
    {
    }

    public virtual void OnPause(){
        isStart = false;
    }

    public void ChangeState(int state, float time = -1f)
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

    public void ForceState(int state, float time = -1f)
    {
        timeState = time;
        currentState = state;
        OnChangeState();
    }

    protected void Update()
    {
        UpdateTimeState();
    }

    protected void FixedUpdate()
    {
        
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

    public int GetState()
    {
        return currentState;
    }
    
    public bool IsStart()
    {
        return isStart;
    }

    protected abstract void OnChangeState();

    protected abstract void OnUpdateState();
    protected abstract void OnFixedUpdateState();
}