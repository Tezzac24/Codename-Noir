using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "ScriptableObjects/Weapons")]
public class WeaponScriptableObject : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float bulletForce;
    public float bulletPattern;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public bool reloading;
}
