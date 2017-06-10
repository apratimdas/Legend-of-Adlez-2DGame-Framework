using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
            collision.gameObject.GetComponent<PlayerHealth2>().TakeDamage(1);
    }


    // Use this for initialization
    void Start()
    {
        StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
