using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 2.5f;
    private bool facingRight = true;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 4f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 2.5f;

    private Rigidbody2D rb;
    public Camera cam;
    private Vector2 movement;
    private Vector2 mousePos;
    private Animator anim;
    public GameObject firepoint;
    private WeaponShooting Ws;

    void Start()
    {
        // what you come here to look at u aint got no buzniuss being here lil boah imma show u a mans world kittenr   
        // Initialises variable 'rb' as Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        anim =  GetComponent<Animator>();
    }

    void Update()
    {
        // disables inputs momentarily while player dashes
        if (isDashing)
        {
            return;
        }

        // stores keyboard input in vector2 movement
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        // sets anims
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
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // on key down dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            anim.SetBool("isDashing", true);
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        // disables inputs momentarily while player dashes
        if (isDashing)
        {
            return;
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        // Makes player sprite face mouse cursor
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;
        firepoint.transform.eulerAngles = new Vector3(0, 0, angle);

    }

    private IEnumerator Dash() 
    {
        canDash = false;
        isDashing = true;
        rb.velocity = movement * dashingPower;

        // momentarily gives iframes for the duration of dashingTime
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(dashingTime);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        isDashing = false;
        yield return new WaitForSeconds(1);
        anim.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashingCooldown-1);
        canDash = true;
    }

    private void flip()
    {
        facingRight = !facingRight; // if var is true it will set to false if false it sets to true
        transform.Rotate(0, 180, 0);

    }
}
