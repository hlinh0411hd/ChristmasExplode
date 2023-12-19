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

    public GameObject bombPrefab;
    GameObject bomb;

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
        bomb = Instantiate(bombPrefab);
    }

    public void UpdateBombEnd(){
        bomb.GetComponent<Bomb>().OnStart();
    }

    public void OnBombPause(){
        bomb.GetComponent<Bomb>().OnPause();
    }
}
