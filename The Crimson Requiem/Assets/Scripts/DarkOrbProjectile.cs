using UnityEngine;

public class DarkOrbProjectile : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("DarkOrbProjectile collided with: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            // Assuming the enemy has a method to take damage
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject); // Destroy the projectile after hitting an enemy
        }
    }
}
