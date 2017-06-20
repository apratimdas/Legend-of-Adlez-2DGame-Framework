using UnityEngine;
using System.Collections;

public class HealthPotionScriptFinal : MonoBehaviour {

    GameObject player;
    PlayerHealthScriptFinal playerHealth;

    // Use this for initialization
    void Start () {
	
	}

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            playerHealth = player.GetComponent<PlayerHealthScriptFinal>();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!player || !player.activeInHierarchy)
            {
                player = GameObject.FindGameObjectWithTag("Player");

                if (!player)
                {
                    return;
                }
                playerHealth = player.GetComponent<PlayerHealthScriptFinal>();
            }

            print("healed");

            if (playerHealth.currentHealth < playerHealth.startingHealth) playerHealth.TakeDamage(-25);

            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
