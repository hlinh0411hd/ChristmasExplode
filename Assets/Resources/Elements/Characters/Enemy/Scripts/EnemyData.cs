using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public float rangeFollow;
    public float maxHealth;
    public float damage;
    public float speed;
    public float shield;
    public float timeCountDownAttack;
    public float timeAttackDuration;
    public float rangeAttack;
}