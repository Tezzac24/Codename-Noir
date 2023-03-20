using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponShoot : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    AIChase ai;

    [SerializeField] float bulletForce = 20f;
    float timer;

    void Start()
    {
        ai = GetComponent<AIChase>();
    }

    void Update()
    {
        // lets enemy shoot in intervals
        timer += Time.deltaTime;

        if (timer > 2 && ai.distance < ai.maxChaseDist)
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
