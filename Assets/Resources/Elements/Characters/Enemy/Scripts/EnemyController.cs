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
    public EnemyData enemyData;
    public static int NUM_ENEMY = 1;
    public static int NUM_BOSS = 0;

    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;

    public List<GameObject> enemies;

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
        UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void CreateEnemy(Vector2Int tile, int id = 0)
    //{
    //    if (id < 0 || id >= enemyPrefabs.Length) return;
    //    Vector2Int tileEmpty = MapController.instance.GetRandomEmptyTile();
    //    GameObject gO = Instantiate(enemyPrefabs[id], MapController.instance.ConvertTileToPoint(tile), Quaternion.identity);
    //    gO.GetComponent<Enemy>()?.OnSpawn();
    //    enemies.Add(gO);
    //}

    //public void CreateBoss(Vector2Int tile, int id = 0)
    //{
    //    if (id < 0 || id >= bossPrefabs.Length) return;
    //    Vector2Int tileEmpty = MapController.instance.GetRandomEmptyTile();
    //    enemies.Add(Instantiate(bossPrefabs[id], MapController.instance.ConvertTileToPoint(tileEmpty), Quaternion.identity));
    //}

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

    void UpdateData(){
        enemyData.maxHealth = 1;
        enemyData.damage = 1;
        enemyData.speed = 1;
        enemyData.shield = 1;
        enemyData.timeCountDownAttack = 1;
        enemyData.timeAttackDuration = 1;
        enemyData.rangeAttack = 1;
    }

    public void UpdateMaxHealth(float m){
        enemyData.maxHealth += m;
    }
    public void UpdateDamage(float d){
        enemyData.damage += d;
    }
    public void UpdateSpeed(float s){
        enemyData.speed += s;
    }
    public void UpdateShield(float s){
        enemyData.shield += s;
    }
    public void UpdateTimeAttack(float t){
        enemyData.timeAttackDuration += t;
    }
}
