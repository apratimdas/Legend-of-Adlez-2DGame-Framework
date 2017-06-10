using UnityEngine;
using System.Collections;

public class SwordSlashScript : MonoBehaviour
{

    public float speed = 3f;
    public float damage = 30f;

    void Update()
    {
        //GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }

    public void OnEnable()
    {
        Invoke("Die", 0.2f);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    // to counter double invokes
    public void OnDisable()
    {
        CancelInvoke();
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Enemy")
    //    {
    //        //Destroy(collision.gameObject);
    //        collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    //        //gameObject.SetActive(false);
    //        Destroy(gameObject);
    //    }
    //}
}
