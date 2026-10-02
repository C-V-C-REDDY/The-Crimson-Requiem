using UnityEngine;

public class DarkOrbEffect : MonoBehaviour , ISpellEffect
{
    public GameObject orbPrefab;
    public float orbSpeed = 20f;
    public Animator animator;
    
    const float firePointOffset = 1f; // Offset to position the fire point slightly in front of the caster
    public void Cast(Vector3 origin, Vector3 direction, SpellData data)
    {
        // animator.SetTrigger("CastAnim");
        // animator.SetTrigger("Attack");
        GameObject orb = Instantiate(orbPrefab, origin, Quaternion.identity);
        Rigidbody rb = orb.GetComponent<Rigidbody>();
        origin.y = firePointOffset; // Keep the y-coordinate the same as the fire point
        rb.linearVelocity = direction.normalized * orbSpeed;

        DarkOrbProjectile projectile = orb.GetComponent<DarkOrbProjectile>();
        projectile.damage = data.damage;

        // Optionally, you can set the damage or other properties from SpellData to the orb here
    }

}
