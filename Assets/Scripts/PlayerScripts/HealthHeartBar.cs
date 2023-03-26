using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    Health hp;
    List<HealthHeart> hearts = new List<HealthHeart>();

    void Awake()
    {
        hp = GameObject.Find("Noir").GetComponent<Health>();
        DrawHearts();
    }

    void OnEnable()
    {
        Health.OnPlayerDamaged += DrawHearts;
    }

    void OnDisable()
    {
        Health.OnPlayerDamaged -= DrawHearts;
    }

    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = hp.startingHealth % 2;
        int heartsToMake = (int)((hp.startingHealth / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(hp.currentHealth - (i*1), 0, 1);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }


    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Full);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}
