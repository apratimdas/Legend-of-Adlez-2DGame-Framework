using UnityEngine;
using System.Collections;

public class EnemyAttack2 : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public float attackDamage = 10f;

    Animator anim;
    GameObject player;
    PlayerHealth2 playerHealth;
    //EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth2>();
        //enemyHealth = player.GetComponent<EnemyHealth>();
        //anim = GetComponent<Animator>();
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
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange /* && enemyHealth.currentHealth > 0*/)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            //anim.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            Debug.Log(this.gameObject.name);
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
