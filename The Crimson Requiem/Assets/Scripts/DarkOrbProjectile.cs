

/// <summary>
/// WHAT: Moves the orb straight, deals damage on trigger with an "Enemy"-tagged collider, then destroys itself.
/// DEPENDS ON: Enemy.TakeDamage(amount, DamageSource.PlayerSpell).
/// NOTE: Orb Direction logic is in DarkOrbEffect.cs, Here only Damage and impact Vfx.
/// <summary>



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
            other.GetComponent<Enemy>().TakeDamage(damage, Enemy.DamageSource.PlayerSpell);

            GameObject impactEffect = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity); // Instantiate the impact effect
            Destroy(impactEffect, impactEffectDuration); // Destroy the impact effect after the specified duration
            Destroy(gameObject); // Destroy the projectile after hitting an enemy
            
        }
    }
}
