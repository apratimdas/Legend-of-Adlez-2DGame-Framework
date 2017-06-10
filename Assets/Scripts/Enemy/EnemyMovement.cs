using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float speed = 5f;
    public Transform player;

    // Use this for initialization
    void Start() {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
	}

    // Update is called once per frame
    void Update() {
        if (player)
        {
            if (!player.gameObject.activeInHierarchy)
            {
                if (GameObject.FindGameObjectWithTag("Player"))
                {
                    player = GameObject.FindGameObjectWithTag("Player").transform;
                }
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
	}

    void FixedUpdate()
    {
        if (player)
        {
            float z = Mathf.Atan2((player.position.y - transform.position.y),
                (player.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

            // have to scale the enemy prefab to -1 on the y axis
            transform.eulerAngles = new Vector3(0, 0, z);

            GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
        }
    }
}
