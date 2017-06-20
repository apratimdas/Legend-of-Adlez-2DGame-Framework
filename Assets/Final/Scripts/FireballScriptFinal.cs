using UnityEngine;
using System.Collections;

public class FireballScriptFinal : MonoBehaviour {

    public float speed = 3f;
    public float damage = 35f;

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
        Destroy(gameObject);
    }

    // to counter double invokes
    public void OnDisable()
    {
        CancelInvoke();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
            collision.transform.GetChild(0).GetComponent<BossShooterScriptFinal>().StopAllCoroutines();

        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            //Destroy(collision.gameObject);
            GameObject obj = (GameObject)Instantiate(hitEffect);

            obj.transform.position = collision.transform.position;
            obj.transform.rotation = Quaternion.identity;
            
            //gameObject.SetActive(false);
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<AudioSource>().Play();

            //Destroy(gameObject);
            if (collision.tag == "Enemy")
                collision.gameObject.GetComponent<EnemyScriptFinal>().TakeDamage(damage);
            else
                collision.GetComponent<TakeHitScriptFinal>().damage(damage);

            Invoke("Die", 2f);
        }
    }
}
