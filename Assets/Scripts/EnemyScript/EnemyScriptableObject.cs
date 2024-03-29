using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public string Name;

    [Header("AIchase")]
    public float maxHealth;
    public float maxChaseDist;
    public float minChaseDist;
    public float speed;

    [Header("Shooting")]
    public float fireRate;
    public float bulletForce;
}
