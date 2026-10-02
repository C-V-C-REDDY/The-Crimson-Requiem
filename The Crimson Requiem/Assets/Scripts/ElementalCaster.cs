using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ElementalCaster : MonoBehaviour
{
    public event System.Action OnCastStarted;

    [SerializeField] WaterSpellData waterData;
    [SerializeField] Volume globalVolume;
    ColorAdjustments colorAdj;
    bool isCasting = false;

    void Awake()
    {
        globalVolume.profile.TryGet(out colorAdj);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            Debug.Log("1 Pressed");
            TryCastWater();
        }
    }

    void TryCastWater()
    {
        if(isCasting) { Debug.Log("Already casting!"); return; }
        if(!SpellEconomy.Instance.TryCastElemental()) return;
        Debug.Log("Cast started.");
        OnCastStarted?.Invoke();
        StartCoroutine(CastWater());
    }

    IEnumerator CastWater()
    {
        isCasting = true;
        var d = waterData;
        GameObject fog = Instantiate(d.fogVFXPrefab, transform.position + d.fogSpawnOffset, Quaternion.identity);
        yield return new WaitForSeconds(d.fogDuration * 0.5f);

        foreach (var enemy in new List<Enemy>(EnemyManager.Instance.activeEnemies))
            if (enemy != null) enemy.ApplySlow(d.slowMlutiplier, d.slowDuration);
        
        Coroutine tint = StartCoroutine(TintRoutine(d));

        yield return new WaitForSeconds(d.fogDuration * 0.5f);
        Destroy(fog);

        yield return tint;
        isCasting = false;
    }

    IEnumerator TintRoutine(WaterSpellData d)
    {
        colorAdj.colorFilter.overrideState = true;
        yield return FadeTint(Color.white, d.tintColor, d.tintFadeDuration);
        yield return new WaitForSeconds(d.tintDuration);
        yield return FadeTint(d.tintColor, Color.white, d.tintFadeDuration);
    }

    IEnumerator FadeTint(Color from, Color to, float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            colorAdj.colorFilter.value = Color.Lerp(from, to, t / time);
            yield return null;
        }
        colorAdj.colorFilter.value = to;
    }

    
}
