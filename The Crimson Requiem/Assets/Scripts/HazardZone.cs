using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HazardZone : MonoBehaviour
{
    HazardData data;

    public void Init(HazardData d)
    {
        data = d;
        StartCoroutine(LifeCycle());
    }

    IEnumerator LifeCycle()
    {
        // juiceManger...
        float elapsed = 0f;
        while(elapsed < data.activeDuaration)
        {
            Tick();
            yield return new WaitForSeconds(data.tickInterval);
            elapsed += data.tickInterval;
        }
        Destroy(gameObject);
    }

    void Tick()
    {
        var hits = Physics.OverlapSphere(transform.position, data.radius, data.targetLayers, QueryTriggerInteraction.Collide);

        var seen =  new HashSet<Component>();

        foreach (var h in hits)
        {
            var e = h.GetComponentInParent<Enemy>();
            if(e != null)
            {
                if(data.affectsEnemies && seen.Add(e)) ApplyToEnemy(e);
                continue;
            }

            var p = h.GetComponentInParent<PlayerManager>();
            if(p != null && data.affectsPlayer && seen.Add(p))
            {
                ApplyToPlayer(p);
            }
        }

        void ApplyToEnemy(Enemy e)
        {
            switch(data.effect)
            {
                case HazardEffect.Damage:
                    e.TakeDamage(data.damage, Enemy.DamageSource.PlayerSpell);
                    break;
                case HazardEffect.Slow:
                    if (data.damage > 0f) e.TakeDamage(data.damage, Enemy.DamageSource.PlayerSpell);
                    e.ApplySlow(data.slowFactor, data.tickInterval * 1.5f);
                    break;
            }
        }

        void ApplyToPlayer(PlayerManager p)
        {
            switch(data.effect)
            {
                case HazardEffect.Damage:
                    p.TakeDamage(data.damage);
                    break;
                case HazardEffect.Slow:
                   p.ApplySlow(data.slowFactor);
                   break;
            }
        }
    }
    
}
