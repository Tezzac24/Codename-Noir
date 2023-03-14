using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float activeMoveSpeed;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 2.5f;

    private Rigidbody2D rb;
    public Camera cam;
    private Vector2 movement;
    private Vector2 mousePos;

    void Start()
    {
        // what you come here to look at u aint got no buzniuss being here lil boah imma show u a mans world kittenr   
        // Initialises variable 'rb' as Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        // Changes mouseposition val from screen pixels to in World coordinates
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        //rb.velocity = movement * activeMoveSpeed;
        rb.MovePosition(rb.position + movement * activeMoveSpeed * Time.fixedDeltaTime);
        
        // gets look direction
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }

    private IEnumerator Dash() 
    {
        canDash = false;
        isDashing = true;
        //rb.gravityScale = 0;
        rb.velocity = movement * dashingPower;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
}
