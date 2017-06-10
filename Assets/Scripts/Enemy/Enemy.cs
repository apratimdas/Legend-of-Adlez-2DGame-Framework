using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject deathEffect;

    public float enemyLevel = 1f;
    public float healthMultiplier = 100f;
    public int points = 10;

    private float currentHealth = 100f;

    bool isDead = false;
    //bool damaged;

    // Use this for initialization
    void Start ()
    {
        currentHealth = healthMultiplier * enemyLevel;
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void setLevel(float level)
    {
        enemyLevel = level;

        currentHealth = healthMultiplier * enemyLevel;
    }

    public void TakeDamage(float damage)
    {
        //damaged = true;

        currentHealth -= damage;

        EnemyMovement em = gameObject.GetComponent<EnemyMovement>();

        if (em)
        {
            Transform player = em.player;

            float z = Mathf.Atan2(-(player.position.y - transform.position.y),
                -(player.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

            // have to scale the enemy prefab to -1 on the y axis
            transform.eulerAngles = new Vector3(0, 0, z);

            GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * em.speed);
        }

        //healthSlider.value = currentHealth;

        //playerAudio.Play();
        //print(currentHealth);
        //print(isDead);
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        Instantiate(deathEffect, transform.position, transform.rotation);

        GameManager.Instance.Score(points);

        gameObject.SetActive(false);

        //playerShooting.DisableEffects();

        //anim.SetTrigger("Die");

        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        //playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }
    
    public void OnEnable()
    {
        isDead = false;
    }
}
