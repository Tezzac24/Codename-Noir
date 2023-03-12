using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed = 5f;
    private Vector2 movement;

    void Start()
    {
        // what you come here to look at u aint got no buzniuss being here lil boah imma show u a mans world kittenr   
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
