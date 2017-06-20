﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnerScriptFinal : MonoBehaviour {

    public PlayerHealthScriptFinal playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public List<Transform> spawnPoints;
    public int enemyLevel = 1;

    // change to approximated maximum number of simultaneous explosions
    public int poolSize = 30;
    public bool poolCanGrow = true;

    private int currLength;

    List<GameObject> pool;

    void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; ++i)
        {
            GameObject obj = (GameObject)Instantiate(enemy);

            obj.SetActive(false);
            obj.transform.parent = transform;
            pool.Add(obj);
        }
    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Spawn()
    {
        currLength = spawnPoints.Count;

        if (!playerHealth.gameObject.activeInHierarchy)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScriptFinal>();
            }
            else
            {
                return;
            }
        }

	    if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, currLength);

        //while (!spawnPoints[spawnPointIndex].gameObject.activeInHierarchy && currLength > 0)
        //{
        //    for (int i = spawnPointIndex; i + 1 < currLength; ++i)
        //    {
        //        spawnPoints[i] = spawnPoints[i + 1];
        //    }
        //    --currLength;
        //    spawnPointIndex = Random.Range(0, currLength);
        //}

        if (currLength > 0 && spawnPoints[spawnPointIndex].gameObject.activeInHierarchy)
        {
            //Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            //GameObject obj = ObjectPoolScript.current.GetPooledObject();
            GameObject obj = GetPoolObject();

            obj.transform.position = spawnPoints[spawnPointIndex].position;
            obj.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            obj.transform.SetParent(transform);
            obj.SetActive(true);

            //print(obj);
            //print(obj.activeSelf);
            //print(obj.activeInHierarchy);
            //print(obj.transform.parent);
            //print(obj.transform.parent);
        }
	}

    GameObject GetPoolObject()
    {
        for (int i = 0; i < pool.Count; ++i)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        if (poolCanGrow)
        {
            GameObject obj = (GameObject)Instantiate(enemy);
            
            //obj.transform.parent = transform;
            pool.Add(obj);

            return obj;
        }

        return null;
    }
}