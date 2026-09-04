using UnityEngine;

public class DarkOrbEffect : MonoBehaviour , ISpellEffect
{
    public GameObject orbPrefab;
    public float orbSpeed = 20f;

    public void Cast(Vector3 origin, Vector3 direction, SpellData data)
    {
        GameObject orb = Instantiate(orbPrefab, origin, Quaternion.identity);
        Rigidbody rb = orb.GetComponent<Rigidbody>();
        rb.linearVelocity = direction.normalized * orbSpeed;

        DarkOrbProjectile projectile = orb.GetComponent<DarkOrbProjectile>();
        projectile.damage = data.damage;

        // Optionally, you can set the damage or other properties from SpellData to the orb here
    }

}
