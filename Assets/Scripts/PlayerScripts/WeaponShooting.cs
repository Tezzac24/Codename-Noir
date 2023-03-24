using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;

    [SerializeField] float bulletForce = 20f;
    Health hp;

    void Start()
    {
        hp = GetComponent<Health>();
    }

    void Update()
    {
        // On left click shoot
        if(Input.GetMouseButtonDown(0) && !hp.isDead)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
