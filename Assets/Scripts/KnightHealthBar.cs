using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class KnightHealthBar : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;

    
	public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

	public void takeDamage(int damage)
	{
        if (this.currentHealth > 0)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth); 
        }
	}

    public void takeRandomDamage(int start, int end)
    {
        int damage = Random.Range(start, end);
        //Debug.Log("Damage:" + damage);
        this.takeDamage(damage);
    }

    public bool isAlive()
    {
        return this.currentHealth > 0;
    }

}
