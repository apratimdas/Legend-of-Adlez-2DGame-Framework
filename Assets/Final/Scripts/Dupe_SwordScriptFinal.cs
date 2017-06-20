using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dupe_SwordScriptFinal : MonoBehaviour
{
    public GameObject explosion;

    // change to approximated maximum number of simultaneous explosions
    public int explosionsPoolSize = 10;
    List<GameObject> explosions;

    void Start()
    {
        explosions = new List<GameObject>();

        for (int i = 0; i < explosionsPoolSize; ++i)
        {
            GameObject obj = (GameObject)Instantiate(explosion);

            obj.SetActive(false);
            explosions.Add(obj);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Instantiate(explosion, collision.transform.position, Quaternion.identity);

        for (int i = 0; i < explosions.Count; ++i)
        {
            if (!explosions[i].activeInHierarchy)
            {
                explosions[i].transform.position = collision.transform.position;
                explosions[i].transform.rotation = Quaternion.identity;
                explosions[i].SetActive(true);

                break;
            }
        }

        Destroy(collision.gameObject);
    }
}
