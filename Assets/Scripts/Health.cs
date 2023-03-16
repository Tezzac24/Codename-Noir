using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float currentHealth;

    private SpriteRenderer sr;
    private Color ogColor;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ogColor = sr.color;
    }

    void Update()
    {
        // Destroys object when health runs out
        if(currentHealth <= 0)
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

    public void takeDamage(float _Damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _Damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // player gets hurt anim
            StartCoroutine(SwitchColor());
        }
        else
        {
            // player is ded anim
        }
    }

    IEnumerator SwitchColor()
    {
        sr.color = new Color(255f, 255f, 255f);
        yield return new WaitForSeconds(0.1f);
        sr.color = ogColor;
    }
}
