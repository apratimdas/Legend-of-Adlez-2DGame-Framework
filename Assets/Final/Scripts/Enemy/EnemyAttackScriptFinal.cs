using UnityEngine;
using System.Collections;

public class EnemyAttackScriptFinal : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10f;

    Animator anim;
    GameObject player;
    PlayerHealthScriptFinal playerHealth;
    bool playerInRange;
    float timer;
    
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
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
    
    void Update()
    {
        if (!player || !player.activeInHierarchy)
        {
            playerInRange = false;
            player = GameObject.FindGameObjectWithTag("Player");

            if (player)
            {
                playerHealth = player.GetComponent<PlayerHealthScriptFinal>();
            }
        }

        timer += Time.deltaTime;

        if (player && timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
