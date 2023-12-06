using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float maxHealth;
    public float damage;
    public float speed;
    public float shield;
    public float critRate;
    public float critDamage;
    public float skillTimeRate;
    public float skillTime;
}