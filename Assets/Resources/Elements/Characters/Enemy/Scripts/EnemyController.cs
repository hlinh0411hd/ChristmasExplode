using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyID
{
    public static int ENEMY_1 = 0;
    public static int ENEMY_2 = 1;
}

public class BossID
{
    public static int BOSS_1 = 0;
}

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public static int NUM_ENEMY = 1;
    public static int NUM_BOSS = 0;

    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;

    public List<GameObject> enemies;

    public GameObject demonPrefab;
    GameObject demon;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
        enemies = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp()
    {
        demon = Instantiate(demonPrefab);
        demon.transform.position = Vector3.zero;
    }

    public void ChangeDemonPosition()
    {
        demon.GetComponent<Demon>().ForceState(DemonState.STEAL);
    }

    public void StartDemonPlay()
    {
        demon.GetComponent<Demon>().OnStart();
    }

    public void OnDestroyEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null && enemies[i].GetComponent<Health>()?.health > 0)
            {
                return;
            }
        }
        ResetEnemies();
        GameController.instance.OnDestroyAllEnemy();
    }

    public void ResetEnemies()
    {
        enemies.Clear();
    }

    public void DestroyAll()
    {
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i] != null)
            {
                Destroy(enemies[i]);
            }
        }
        ResetEnemies();
    }

    public void OnStart()
    {
        enemies.ForEach(e => { 
            e.GetComponent<Enemy>()?.OnStart();
        });
    }

}
