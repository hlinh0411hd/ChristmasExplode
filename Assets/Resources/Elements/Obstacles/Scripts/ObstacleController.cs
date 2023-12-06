using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleID{
    public const int TREE = 0;
    public const int ROCK = 10;
}

public class ObstacleController
{
    public static ObstacleController instance = null;

    public GameObject bombPrefabs;
    public GameObject bomb;

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
        bomb = Instanciate(bombPrefabs);
    }

    public void UpdateBombEnd(){

    }
}
