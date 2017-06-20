using UnityEngine;
using System.Collections;

public class CameraMovementScriptFinal : MonoBehaviour {

    [SerializeField]
    private Transform player;

	// Use this for initialization
	void Start ()
    {
        if (!player || !player.gameObject.activeInHierarchy)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else
            {
                return;
            }
        }

        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!player || !player.gameObject.activeInHierarchy)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else
            {
                return;
            }
        }

        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
