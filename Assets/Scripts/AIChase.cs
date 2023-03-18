using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    
    [SerializeField] float speed;
    public float distance;
    public float chaseDist = 7;

    void Start()
    {
        
    }

    void Update()
    {
        // gets dist, dir and angle
        distance = Vector2.Distance(this.transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // sets the distance at which the enemy will chase the player
        if (distance < chaseDist)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);                
            }
    }
}
