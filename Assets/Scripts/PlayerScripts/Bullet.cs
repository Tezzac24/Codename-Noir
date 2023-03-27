using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject hitEffect;

    void Update()
    {
        Physics2D.IgnoreLayerCollision(7, 7, true);
        Physics2D.IgnoreLayerCollision(7, 6, true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
