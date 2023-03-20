using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public GameObject firepoint;
    
    [SerializeField] float speed;
    public float distance;
    public float maxChaseDist = 7;
    public float minChaseDist = 1;
    bool facingRight = true;

    void Start()
    {

    }

    void FixedUpdate()
    {
        // gets dist, dir and angle
        distance = Vector2.Distance(this.transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        //direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // sets the distance at which the enemy will chase the player
        if (distance < maxChaseDist && distance > minChaseDist)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);             
            firepoint.transform.eulerAngles = new Vector3(0, 0, angle);  
        }


        if (direction.x <= 0 && facingRight)
        {
            flip();
        } 
        else if (direction.x >= 0 && !facingRight)
        {
           flip();
        }
    }

    void flip()
    {
        facingRight = !facingRight; // if var is true it will set to false if false it sets to true
        transform.Rotate(0, 180, 0);

    }
}
