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

    float timeIncreaseSpeed;


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

    void Update(){
        UpdateTime();
    }

    void UpdateTime(){
        if (timeIncreaseSpeed >= 0){
            timeIncreaseSpeed -= Time.deltaTime;
            if (timeIncreaseSpeed < 0){
                playerData.speed = playerDataBase.speed;
            }
        }
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
        playerData.heightJump = playerDataBase.heightJump;
        playerData.numSnow = playerDataBase.numShow;
        playerData.numActivator = playerDataBase.numActivator;
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
        timeIncreaseSpeed = 0;
    }

    public void OnPause(){
        crrPlayer?.GetComponent<Player>()?.OnPause();
    }

    public GameObject GetPlayer()
    {
        return crrPlayer;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void AddNumSnow(int n){
        playerData.numSnow += n;
        if (playerData.numShow > 10){
            playerData.numShow = 10;
        }
    }
    public void IncreaseSpeed(){
        playerData.speed += 1;
        timeIncreaseSpeed = 3;
    }

    public void IncreaseActivator(){
        playerData.numActivator += 1;
    }
}
