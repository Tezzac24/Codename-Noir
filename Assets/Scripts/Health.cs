using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        // Destroys object when health runs out
        if(/*gameObject.CompareTag("Enemy") && */currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // on collision with a bullet gameobject takes damage
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            takeDamage(20);
        }
    }

    public float takeDamage(float _Damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _Damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // player gets hurt anim
        }
        else
        {
            // player is ded anim
        }
        return 0f;
    }
}
