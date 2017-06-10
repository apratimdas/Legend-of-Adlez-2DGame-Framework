using UnityEngine;
using System.Collections;

public class movet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(
            Random.Range(-4, 4),
            Random.Range(-4, 4),
            Random.Range(-1, 1)
         );

    }
}
