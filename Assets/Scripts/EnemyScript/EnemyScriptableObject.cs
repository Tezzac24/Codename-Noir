using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public float maxHealth;
    public float maxChaseDist;
    public float minChaseDist;
    //public bool facingRight = true;
}
