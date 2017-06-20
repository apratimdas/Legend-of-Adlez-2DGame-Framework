using UnityEngine;
using System.Collections;

public class EnemyIsometricMovementScriptFinal : MonoBehaviour {

    public float speed = 0.1f;
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
            transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
        }
    }
}
