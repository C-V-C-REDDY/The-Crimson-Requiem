using UnityEngine;

public class DarkOrbProjectile : MonoBehaviour
{
    public float damage;
    [SerializeField] private float impactEffectDuration = 1f; // Duration for which the impact effect will be visible
    [SerializeField] private GameObject impactEffectPrefab; // Prefab for the impact effect

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("DarkOrbProjectile collided with: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            // Assuming the enemy has a method to take damage
            other.GetComponent<Enemy>().TakeDamage(damage);

            GameObject impactEffect = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity); // Instantiate the impact effect
            Destroy(impactEffect, impactEffectDuration); // Destroy the impact effect after the specified duration
            Destroy(gameObject); // Destroy the projectile after hitting an enemy
            
        }
    }
}
