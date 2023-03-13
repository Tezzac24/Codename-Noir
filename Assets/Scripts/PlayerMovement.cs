using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;

    private Rigidbody2D rb;
    public Camera cam;
    private Vector2 movement;
    private Vector2 mousePos;

    void Start()
    {
        // what you come here to look at u aint got no buzniuss being here lil boah imma show u a mans world kittenr   
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log(mousePos);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }
}
