using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private Transform player;

	// Use this for initialization
	void Start () {

        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

    }
	
	// Update is called once per frame
	void Update () {


        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

    }
}
