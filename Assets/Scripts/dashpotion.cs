using UnityEngine;
using System.Collections;

public class dashpotion : MonoBehaviour {

    GameObject player;

    // Use this for initialization
    void Start () {
	
	}

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            print("Boosted");
            player.GetComponent<PlayerController>().boosted();
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
