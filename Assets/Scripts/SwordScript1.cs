using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SwordScript1 : MonoBehaviour
{
    public GameObject explosion;
    [SerializeField]
    public Text score;

    private int scorenum = 0;

    // change to approximated maximum number of simultaneous explosions
    public int explosionsPoolSize = 10;
    public bool poolCanGrow = true;

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
        //Instantiate(explosion, collision.transform.position, Quaternion.identity);

        //GameObject obj = ObjectPoolScript.current.GetPooledObject();

        //if (obj == null)
        //{
        //    return;
        //}

        //obj.transform.position = collision.transform.position;
        //obj.transform.rotation = Quaternion.identity;
        //obj.SetActive(true);

        //print(obj.transform.parent);

        //print("GO Hier: " + gameObject.activeInHierarchy);
        //print("GO Self: " + gameObject.activeSelf);
        //print("OB Hier: " + obj.activeInHierarchy);
        //print("OB Self: " + obj.activeSelf);
        //print(obj.name);

        /////////////////////////////////////

        if (collision.tag == "Wall")
            return;
        if (collision.tag == "Boss")
            collision.transform.GetChild(0).GetComponent<Shooter>().StopAllCoroutines();

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

        if (collision.tag == "Boss")
            collision.GetComponent<TakeHit>().damage(10f);

        if (collision.tag != "Boss")
        {
            if (collision.tag == "Enemy")
            {
                scorenum += 10;
                score.text = "Score : " + scorenum.ToString();
            }

            collision.gameObject.SetActive(false);
        }
    }
}
