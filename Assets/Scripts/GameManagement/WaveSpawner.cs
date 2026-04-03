using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class WaveSpawner : MonoBehaviour
{
   
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
 
    public Transform[] spawnLocation;
    public int spawnIndex;
 
    public int waveDuration;
    float waveTimer;
    float spawnInterval;
    float spawnTimer;
 
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        GenerateWave();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnTimer <=0)
        {
            //spawn an enemy
            if(enemiesToSpawn.Count >0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position,Quaternion.identity); // spawn first enemy in our list
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
 
                if(spawnIndex + 1 <= spawnLocation.Length-1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
 
        if(waveTimer<=0 && spawnedEnemies.Count <=0)
        {
            currWave++;
            GenerateWave();
        }
    }
 
    public void GenerateWave()
    {
        if (enemies.Count == 0)
        {
            Debug.LogError("WaveSpawner has no enemy definitions. Disabling spawner.");
            enabled = false;
            return;
        }

        waveValue = Mathf.Max(currWave, 1) * 30;
        GenerateEnemies();

        if (enemiesToSpawn.Count == 0)
        {
            Debug.LogWarning($"Wave {currWave} generated zero enemies. Ending wave early.");
            spawnInterval = waveDuration;
            waveTimer = 0f;
            return;
        }

        spawnInterval = Mathf.Max(0.01f, (float)waveDuration / enemiesToSpawn.Count); // gives a fixed time between each enemies
        waveTimer = waveDuration; // wave duration is read only
    }
 
    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.
 
        // repeat... 
 
        //  -> if we have no points left, leave the loop
 
        List<GameObject> generatedEnemies = new List<GameObject>();
        int safetyCounter = 0;
        while(waveValue > 0 && generatedEnemies.Count < 50 && safetyCounter < 500)
        {
            safetyCounter++;
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if(waveValue-randEnemyCost>=0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if(waveValue<=0)
            {
                break;
            }
        }

        if (safetyCounter >= 500)
        {
            Debug.LogWarning("WaveSpawner stopped enemy generation after reaching safety iteration limit.");
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
  
}
 
[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
