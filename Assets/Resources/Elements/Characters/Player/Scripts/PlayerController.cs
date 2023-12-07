using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;
    public PlayerData playerDataBase;
    public PlayerData playerData;
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

    public void SetUp(){
        UpdateData();
        InitPlayer();
    }

    void UpdateData(){
        playerData.maxHealth = playerDataBase.maxHealth;
        playerData.speed = playerDataBase.speed;
        playerData.attackTime = playerDataBase.attackTime;
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
}
