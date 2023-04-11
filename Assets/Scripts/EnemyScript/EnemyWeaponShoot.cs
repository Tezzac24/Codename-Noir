using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponShoot : MonoBehaviour
{
    public EnemyScriptableObject enemySO;
    public Transform firepoint;
    public GameObject bulletPrefab;
    AIChase ai;
    Health hp;
    ParticleSystem particle;

    float timer;

    void Start()
    {
        ai = GetComponent<AIChase>();
        hp = GameObject.Find("Noir").GetComponent<Health>();
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // lets enemy shoot in intervals
        timer += Time.deltaTime;

        if (timer > enemySO.fireRate && ai.distance < enemySO.maxChaseDist && !hp.isDead)
        {
            //particle.Play(true);
            Shoot(); 
            timer = 0;
        }
        else
        {
            //particle.Stop(true);
        }
    }

    // Instantiates and fires bullets
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * enemySO.bulletForce, ForceMode2D.Impulse);
    }
}
