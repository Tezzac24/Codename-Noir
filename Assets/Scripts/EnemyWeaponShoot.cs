using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponShoot : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    private float timer;

    void Start()
    {

    }

    void Update()
    {
        // lets enemy shoot in intervals
        timer += Time.deltaTime;

        if (timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }

    // Instantiates and fires bullets
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
