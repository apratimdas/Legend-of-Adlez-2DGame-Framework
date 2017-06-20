using UnityEngine;
using System.Collections;

public class EnemyScript_NTM : MonoBehaviour {

    public float speed = 5f;
    public Transform player;
    public float enemysenserange = 4f;
    private float enemyrange;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {

        enemyrange = Vector3.Distance(transform.position, player.transform.position);

        //Debug.Log(enemyrange);
        if (enemyrange < enemysenserange)
        {
            enemysenserange = 100f;
            float z = Mathf.Atan2((player.position.y - transform.position.y),
                (player.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

            transform.eulerAngles = new Vector3(0, 0, z);

            GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
        }

    }
}
