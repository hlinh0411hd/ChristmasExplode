using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public bool isStart;


    private void Start()
    {
        StartCountDown();
    }

    public void StartCountDown()
    {
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart)
        {
            return;
        }
        this.time -= Time.deltaTime;
        if (this.time <= 0)
        {
            IPool pool = GetComponent<IPool>();
            if (pool != null)
            {
                pool?.OnRemove();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
