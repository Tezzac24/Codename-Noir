using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static event Action OnPlayerDamaged;

    public float startingHealth;
    public float currentHealth;
    public bool isDead = false;

    SpriteRenderer sr;
    Color ogColor;
    Animator anim;

    HealthHeartBar hpBar;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        currentHealth = startingHealth;
        hpBar = GameObject.Find("HealthHearts").GetComponent<HealthHeartBar>();
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ogColor = sr.color;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Destroys object when health runs out
        if(currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("Game over shit kid do better");
        }
    }

    // on collision with a bullet gameobject takes damage
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            takeDamage(1);
            //hpBar.DrawHearts();
            OnPlayerDamaged?.Invoke();
        }
    }

    void takeDamage(float _Damage)
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
            StartCoroutine(Death());
        }
    }

    IEnumerator SwitchColor()
    {
        sr.color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = ogColor;
    }

    IEnumerator Death()
    {
        anim.SetBool("isDying", true);
        yield return new WaitForSeconds(.5f);
        anim.SetBool("isDying", false);
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }
}
