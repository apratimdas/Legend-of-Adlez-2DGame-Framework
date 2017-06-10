using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour {

    public float speed = 3f;
    public float damage = 30f;

    public GameObject hitEffect;

    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }

    public void OnEnable()
    {
        Invoke("Die", 3f);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //Destroy(collision.gameObject);
            GameObject obj = (GameObject)Instantiate(hitEffect);

            obj.transform.position = collision.transform.position;
            obj.transform.rotation = Quaternion.identity;

            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
