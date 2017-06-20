using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthScriptFinal : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    
    bool isDead;
    bool damaged;
    
    void Awake ()
    {
        currentHealth = startingHealth;
    }
	
	void Update ()
    {
	    if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
	}

    public void TakeDamage(float damage)
    {
        damaged = true;

        currentHealth -= damage;

        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        GameManagerScriptFinal.Instance.GameOver();
    }
}
