using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleID{
    public const int TREE = 0;
    public const int ROCK = 10;
}

public class ObstacleController: MonoBehaviour
{
    public static ObstacleController instance = null;


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

    void Start(){
    }

    public void UpdateBombEnd(){

    }
}
