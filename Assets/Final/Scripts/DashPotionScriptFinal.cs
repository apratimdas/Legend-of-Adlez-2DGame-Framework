using UnityEngine;
using System.Collections;

public class DashPotionScriptFinal : MonoBehaviour {

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
        if (other.gameObject.tag == "Player")
        {
            if (!player || !player.activeInHierarchy)
            {
                player = GameObject.FindGameObjectWithTag("Player");

                if (!player)
                {
                    return;
                }
            }

            print("Boosted");

            player.GetComponent<PlayerControllerScriptFinal>().boosted();

            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
