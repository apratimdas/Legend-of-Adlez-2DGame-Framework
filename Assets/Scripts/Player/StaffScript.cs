using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaffScript : MonoBehaviour
{
    [Tooltip("Put the attack effect here")]
    [SerializeField]
    private GameObject explosion;

    // change to approximated maximum number of simultaneous explosions
    public int explosionsPoolSize = 10;
    public bool poolCanGrow = true;

    public float weaponDamage = 10f;

    List<GameObject> explosions;

    void Awake()
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
        if (collision.tag == "Enemy")
        {
            bool found = false;

            for (int i = 0; i < explosions.Count && !found; ++i)
            {
                if (!explosions[i].activeInHierarchy)
                {
                    explosions[i].transform.position = collision.transform.position;
                    explosions[i].transform.rotation = Quaternion.identity;
                    explosions[i].SetActive(true);

                    found = true;
                }
            }

            if (!found && poolCanGrow)
            {
                GameObject obj = (GameObject)Instantiate(explosion);

                obj.transform.position = collision.transform.position;
                obj.transform.rotation = Quaternion.identity;
                obj.SetActive(true);

                explosions.Add(obj);
            }

            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);
        }
    }
}
