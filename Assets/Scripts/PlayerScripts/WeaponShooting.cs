using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponShooting : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    // public Transform firepoint;
    // public Transform gunEndPoint;
    // [SerializeField] GameObject bulletPrefab;
    Health hp;

    [SerializeField] WeaponScriptableObject weaponSO;

    void Start()
    {
        hp = GetComponent<Health>();
    }

    void Update()
    {
        // On left click shoot
        if(Input.GetMouseButtonDown(0) && !hp.isDead)
        {
            shootInput?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            reloadInput?.Invoke();
        }
    }
}
