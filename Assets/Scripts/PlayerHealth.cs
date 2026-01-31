using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 3;

    public SpriteRenderer playerSR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        BarChallenge.Instance._actionFail += TakeDamage;

        currentHealth = maxHealth;
    }

    private void TakeDamage() { TakeDamage(1); }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        playerSR.enabled = false;
        BarChallenge.Instance.SetObjectState(false);
    }
    
}
