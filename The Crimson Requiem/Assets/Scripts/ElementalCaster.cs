using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ElementalCaster : MonoBehaviour
{
    public event System.Action OnCastStarted;

    [SerializeField] WaterSpellData waterData;
    [SerializeField] WindSpellData windData;
    [SerializeField] IceSpellData iceData;
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(SpellEconomy.Instance.TryCastElemental())
            {
                Debug.Log("2 Pressed");
                OnCastStarted?.Invoke();
                StartCoroutine(CastWind(windData));
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) TryCastIce();
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
        
        Coroutine tint = StartCoroutine(TintRoutine(d.tintColor, d.tintFadeDuration, d.tintDuration));

        yield return new WaitForSeconds(d.fogDuration * 0.5f);
        Destroy(fog);

        yield return tint;
        isCasting = false;
    }

    IEnumerator TintRoutine(Color color, float fade, float hold)
    {
        colorAdj.colorFilter.overrideState = true;
        yield return FadeTint(Color.white, color, fade);
        yield return new WaitForSeconds(hold);
        yield return FadeTint(color, Color.white, fade);
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

    private IEnumerator CastWind(WindSpellData data)
    {
        GameObject tornado = Instantiate(data.tornadoPrefab, data.tornadoPosition, Quaternion.identity);
        var enemies = new List<Enemy>(EnemyManager.Instance.activeEnemies);
        foreach(Enemy e in enemies)
        {
            if(e != null) e.ApplyTornado(data);
        }
        yield return new WaitForSeconds(data.pullInDuration + data.orbitDuration);
        Destroy(tornado);
    }

    void TryCastIce()
    {
        if(isCasting) return;
        if(!SpellEconomy.Instance.TryCastElemental()) return;
        OnCastStarted?.Invoke();
        StartCoroutine(CastIce(iceData));
    }

    IEnumerator CastIce(IceSpellData data)
    {
        isCasting = true;

        GameObject vfx = Instantiate(data.iceVFXPrefab, data.vfxPosition, Quaternion.identity);
        Destroy(vfx, data.vfxLifetime + 0.5f);
        JuiceManager.Instance.ScreenShake(data.shakeforce);

        yield return new WaitForSeconds(data.impactDelay);
        // JuiceManager.Instance.Hitstop(0.1f);

        foreach(Enemy e in new List<Enemy>(EnemyManager.Instance.activeEnemies))
        {
            if(e != null) e.ApplySlow(0f, data.freezeDuration);
        }
        yield return StartCoroutine(TintRoutine(data.tintColor, data.tintFadeDuration, data.tintDuration));
        isCasting = false;
    }

    
}
