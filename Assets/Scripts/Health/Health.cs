using UnityEngine;

public class Health : MonoBehaviour
{
    // Variables
    public float currentHealth;
    public float maxHealth;
    public Death death;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        death = GetComponent<Death>();
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth); // Ensure our health stays in regular bounds.
        if (currentHealth <= 0) {
            Die (source);
        }

        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);
    }

    public virtual void Heal(float amount, Pawn source)
    {
        currentHealth = currentHealth + amount;
        currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth); // Ensure our health stays in regular bounds.

        Debug.Log(source.name + " healed " + amount + " health to " + gameObject.name);
    }

    public void Die (Pawn source)
    {
        if (death != null)
        {
            death.Die();            
        }
    }
}
