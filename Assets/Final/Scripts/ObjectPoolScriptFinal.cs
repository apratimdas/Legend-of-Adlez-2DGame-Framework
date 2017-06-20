using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolScriptFinal : MonoBehaviour {

    public static ObjectPoolScriptFinal current;

    public GameObject pooledObj;
    public int poolSize = 10;
    public bool canGrow = true;

    List<GameObject> pool;

    void Awake()
    {
        current = this;
    }

    // Use this for initialization
    void Start ()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; ++i)
        {
            GameObject obj = (GameObject)Instantiate(pooledObj);

            obj.SetActive(false);
            obj.transform.parent = transform;
            pool.Add(pooledObj);
        }
	}

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; ++i)
        {
            //print(i + "OB Hier: " + pool[i].activeInHierarchy);
            //print(i + "OB Self: " + pool[i].activeSelf);
            //print(pool[i].activeInHierarchy);

            //if (!pool[i].activeSelf)
            if (!pool[i].activeInHierarchy)
            {
                //pool[i].SetActive(true);
                print("In");

                return pool[i];
            }
        }

        if (canGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObj);

            //obj.SetActive(false);
            obj.transform.parent = transform;
            pool.Add(obj);
            print(obj.activeSelf);
            print(obj.activeInHierarchy);

            return obj;
        }

        return null;
    }
}
