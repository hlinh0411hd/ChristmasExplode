using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;
    public PlayerData playerDataBase;
    public PlayerData playerData;
    public int exp;
    public int level;

    public GameObject playerPrefab;

    GameObject crrPlayer;


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

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartGame(){
        UpdateData();
        exp = 0;
        level = 0;
        InitPlayer();
    }

    void UpdateData(){
        playerData.maxHealth = playerDataBase.maxHealth;
        playerData.damage = playerDataBase.damage;
        playerData.speed = playerDataBase.speed;
        playerData.shield = playerDataBase.shield;
        playerData.critRate = playerDataBase.critRate;
        playerData.critDamage = playerDataBase.critDamage;
        playerData.skillTimeRate = playerDataBase.skillTimeRate;
        playerData.skillTime = playerDataBase.skillTime;
    }

    public void InitPlayer()
    {
        if (crrPlayer == null)
        {
            crrPlayer = Instantiate(playerPrefab);
        }
        crrPlayer?.SetActive(false);
    }

    public void StartPlay()
    {
        crrPlayer?.SetActive(true);
        crrPlayer?.GetComponent<Player>().OnStart();
    }

    public GameObject GetPlayer()
    {
        return crrPlayer;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void AddExp(int e){
        exp += e;
        CheckLevelUp();
    }

    public void CheckLevelUp(){
        if (exp >= 10){
            level += 1;
            exp -= 10;
        }
    }
}
