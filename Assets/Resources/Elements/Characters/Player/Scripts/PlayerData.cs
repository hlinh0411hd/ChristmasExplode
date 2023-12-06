using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public float skillTime;
    public float attackTime;
}