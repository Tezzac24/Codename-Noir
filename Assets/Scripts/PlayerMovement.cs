using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 2.5f;
    bool facingRight = true;

    [Header("dashing")]
    bool canDash = true;
    bool isDashing;
    [SerializeField] private float dashingPower = 4f;
    float dashingTime = 0.2f;
    float dashingCooldown = 2.5f;

    Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    Animator anim;
    WeaponShooting ws;
    Health hp;

    void Start()
    {
        // what you come here to look at u aint got no buzniuss being here lil boah imma show u a mans world kittenr   
        // Initialises variable 'rb' as Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        anim =  GetComponent<Animator>();
        ws = GetComponent<WeaponShooting>();
        hp = GetComponent<Health>();
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        // disables inputs momentarily while player dashes
        if (isDashing)
        {
            return;
        }

        // stores keyboard input in vector2 movement
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        if(!hp.isDead)
        {
            // sets moving anims
            if (movement != new Vector2(0,0))
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            // flip func
            if(movement.x > 0 && facingRight) // if you move left but are facing right flip left
            {
                flip();
            }
            else if (movement.x < 0 && !facingRight)
            {
                flip();
            }

            // Changes mouseposition val from screen pixels to in World coordinates

            // on key down dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                anim.SetBool("isDashing", true);
                StartCoroutine(Dash());
            }
        }

        if (hp.isDead)
        {
            movement = new Vector2(0,0);
            Physics2D.IgnoreLayerCollision(6, 8, true);
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // disables inputs momentarily while player dashes
        if (isDashing)
        {
            return;
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        // Aims firepoint
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;
        ws.firepoint.transform.eulerAngles = new Vector3(0, 0, angle);

    }

    IEnumerator Dash() 
    {
        canDash = false;
        isDashing = true;
        rb.velocity = movement * dashingPower;

        // momentarily gives iframes for the duration of dashingTime
        Physics2D.IgnoreLayerCollision(6, 8, true);
        yield return new WaitForSeconds(dashingTime);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        isDashing = false;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashingCooldown-0.5f);
        canDash = true;
    }

    void flip()
    {
        facingRight = !facingRight; // if var is true it will set to false if false it sets to true
        transform.Rotate(0, 180, 0);

    }
}
