using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;     
    public int currentHealth;       

    private void Start()
    {
        currentHealth = maxHealth;  
    }

    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;  

        if (currentHealth <= 0)
        {
            Die();  
        }
    }

   
    private void Die()
    {
       
        Debug.Log("Character has died!");
    }
}