using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public Transform Camera;
    Coroutine spawnRoutine;

    [Header("Enemy params")]
    [SerializeField] private int enemyCount;
    [SerializeField] private int MaxEnemy = 10;
    
    [SerializeField] private float waitTime = 2; 

    void Start()
    {
        spawnRoutine = StartCoroutine(EnemySpawnLoop());
    }

    void OnDisable()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator EnemySpawnLoop()
    {
        while (true)
        {
            enemyCount = FindObjectsOfType<EnemyWeaponShoot>().Length;

            if (enemyCount <= MaxEnemy && enemies.Count > 0)
            {
                Vector3 enemySpawn = new Vector3(Camera.position.x, Camera.position.y + 3, 0);

                int index = Random.Range(0, enemies.Count);

                Instantiate(enemies[index], enemySpawn, Quaternion.identity);
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
