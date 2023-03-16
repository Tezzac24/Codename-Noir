using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform Camera;
    
    [SerializeField] private float waitTime = 2; 

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            Vector3 enemySpawn = new Vector3(Camera.position.x + 5, Camera.position.y + 3, 0);
            Instantiate(enemies[0], enemySpawn, Quaternion.identity);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
