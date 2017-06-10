using UnityEngine;
using System.Collections;

public class healthpotion : MonoBehaviour {

    GameObject player;
    PlayerHealth2 playerHealth;

    // Use this for initialization
    void Start () {
	
	}

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth2>();

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            print("healed");
            if (playerHealth.currentHealth < playerHealth.startingHealth) playerHealth.TakeDamage(-25);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
