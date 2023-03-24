using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PeashooterSO", menuName = "ScriptableObjects/Enemy/Peashooter")]
public class EnemyScriptableObject : ScriptableObject
{
    public float maxHealth;
    public float maxChaseDist;
    public float minChaseDist;
    public bool facingRight;
}
