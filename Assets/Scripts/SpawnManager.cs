using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public Transform Camera;

    [Header("Enemy params")]
    [SerializeField] private int enemyCount;
    [SerializeField] private int MaxEnemy = 10;
    
    [SerializeField] private float waitTime = 2; 

    void Start()
    {
        //StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyWeaponShoot>().Length;

        if (enemyCount <= MaxEnemy)
        {
            StartCoroutine(EnemySpawn());
        }
        else
        {
            StopCoroutine(EnemySpawn());
        }
    }

    private IEnumerator EnemySpawn()
    {
        // while (true)
        // {
            Vector3 enemySpawn = new Vector3(Camera.position.x, Camera.position.y + 3, 0);

            int index = Random.Range(0, enemies.Count);

            yield return new WaitForSeconds(waitTime);
            Instantiate(enemies[index], enemySpawn, Quaternion.identity);
        //}
    }
}
