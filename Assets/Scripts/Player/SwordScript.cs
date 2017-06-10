using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordScript : MonoBehaviour
{
    [Tooltip("Put the attack effect here")]
    [SerializeField]
    private GameObject hitEffect;

    // change to approximated maximum number of simultaneous explosions
    public int explosionsPoolSize = 10;
    public bool poolCanGrow = true;

    public float weaponDamage = 35f;

    List<GameObject> effects;

    void Start()
    {
        effects = new List<GameObject>();

        for (int i = 0; i < explosionsPoolSize; ++i)
        {
            GameObject obj = (GameObject)Instantiate(hitEffect);

            obj.SetActive(false);
            effects.Add(obj);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            bool found = false;

            for (int i = 0; i < effects.Count && !found; ++i)
            {
                if (!effects[i].activeInHierarchy)
                {
                    effects[i].transform.position = collision.transform.position;
                    effects[i].transform.rotation = Quaternion.identity;
                    effects[i].SetActive(true);

                    found = true;
                }
            }

            if (!found && poolCanGrow)
            {
                GameObject obj = (GameObject)Instantiate(hitEffect);

                obj.transform.position = collision.transform.position;
                obj.transform.rotation = Quaternion.identity;
                obj.SetActive(true);

                effects.Add(obj);
            }

            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(weaponDamage);
        }
    }
}
