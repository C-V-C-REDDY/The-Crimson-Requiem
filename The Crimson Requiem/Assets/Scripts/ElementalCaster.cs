using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ElementalCaster : MonoBehaviour
{
    public event System.Action OnCastStarted;
    public static ElementalCaster Instance;

    [SerializeField] WaterSpellData waterData;
    [SerializeField] WindSpellData windData;
    [SerializeField] IceSpellData iceData;
    [SerializeField] LightningSpellData lightningData;
    [SerializeField] FireSpellData fireSpellData;
    [SerializeField] EarthSpellData earthSpellData;
    [SerializeField] GalaxyTourData galaxyTourData;
    [SerializeField] Volume globalVolume;
    ColorAdjustments colorAdj;
    bool isCasting = false;
    bool galaxyUnloucked = false;
    void OnEnable() {EnemySpawner.OnFloodWaveFullySpawned += UnlockGalaxy;}
    void OnDisable() {EnemySpawner.OnFloodWaveFullySpawned -= UnlockGalaxy;}

    public  event System.Action OnCastWater;
    public  event System.Action OnCastWind;
    public  event System.Action OnCastFire;
    public  event System.Action OnCastLightning;
    public  event System.Action OnCastIce;
    public  event System.Action OnCastEarth;
    public  event System.Action OnCastGalaxy;

    void UnlockGalaxy() {galaxyUnloucked = true; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        if(Input.GetKeyDown(KeyCode.Alpha5)) TryCastIce();
        if(Input.GetKeyDown(KeyCode.Alpha4)) TryCastLightning();
        if(Input.GetKeyDown(KeyCode.Alpha3)) TryCastFire();
        if(Input.GetKeyDown(KeyCode.Alpha6)) TryCastEarth();
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            if(!galaxyUnloucked) return;
             TryCastGalaxyTour();
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
        AudioManager.Instance.Play(AudioManager.Instance.spellCast);
        ToastUI.Instance.Show("Casting WaterFogSpell : Enemies Slowed");
        OnCastWater?.Invoke();
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

    IEnumerator TintRoutine(Color color, float fadeIn, float hold, float fadeOut)
    {
        colorAdj.colorFilter.overrideState = true;
        yield return FadeTint(Color.white, color, fadeIn);
        yield return new WaitForSeconds(hold);
        yield return FadeTint(color, Color.white, fadeOut);
    }
    IEnumerator TintRoutine(Color color, float fade, float hold) => TintRoutine(color, fade, hold, fade);

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
        AudioManager.Instance.Play(AudioManager.Instance.spellCast);
        OnCastWind?.Invoke();
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
        AudioManager.Instance.Play(AudioManager.Instance.spellCast);
        OnCastIce?.Invoke();
        ToastUI.Instance.Show("Casting IceSpell : Enemies Frozen");
        isCasting = true;

        GameObject vfx = Instantiate(data.iceVFXPrefab, data.vfxPosition, Quaternion.identity);
        Destroy(vfx, data.vfxLifetime + 0.5f);
        JuiceManager.Instance.ScreenShake(data.shakeforce, 0.1f);

        yield return new WaitForSeconds(data.impactDelay);
        // JuiceManager.Instance.Hitstop(0.1f);

        foreach(Enemy e in new List<Enemy>(EnemyManager.Instance.activeEnemies))
        {
            if(e != null) e.ApplySlow(0f, data.freezeDuration);
        }
        yield return StartCoroutine(TintRoutine(data.tintColor, data.tintFadeDuration, data.tintDuration, data.tintFadeDuration));
        isCasting = false;
    }

    void TryCastLightning()
    {
        if(isCasting) return;
        if(!SpellEconomy.Instance.TryCastElemental()) return;
        OnCastStarted?.Invoke();
        StartCoroutine(CastLightning(lightningData));
    }

    IEnumerator CastLightning(LightningSpellData d)
    {
        AudioManager.Instance.Play(AudioManager.Instance.spellCast);
        OnCastLightning?.Invoke();
        ToastUI.Instance.Show("Casting LightningSpell : Heavy Damage On All Enemies");
        isCasting = true;
        colorAdj.colorFilter.overrideState = true;

        yield return FadeTint(Color.white, d.dimColor, d.telegraphTime);
        JuiceManager.Instance.ScreenShake(d.shakeForce, d.shakeDuration);
        JuiceManager.Instance.Hitstop(d.hitStopTime);
        // Coroutine tint = StartCoroutine(TintRoutine(d.tintColor, d.tintFadeIn, d.tintHold, d.tintFadeOut));
        
        foreach(var enemy in new List<Enemy>(EnemyManager.Instance.activeEnemies))
           if(enemy != null) StartCoroutine(StrikeEnemy(enemy, d));
        
        yield return FadeTint(d.dimColor, d.tintColor, d.tintFadeIn);
        yield return new WaitForSeconds(d.tintHold);
        yield return FadeTint(d.tintColor, Color.white, d.tintFadeOut);
        isCasting = false;
    }

    IEnumerator StrikeEnemy(Enemy e, LightningSpellData d)
    {
        yield return new WaitForSeconds(Random.Range(0f, d.maxStrikeStagger));
        if(e == null) yield break;

        GameObject strikeEffect = Instantiate(d.strikeVfxPrefab, e.transform.position, Quaternion.identity);
        JuiceManager.Instance.ScreenShake(d.strikeShake, 0.25f);
        Destroy(strikeEffect, d.shockDuration + 0.5f);
        e.ApplyShock(d.shakeDuration, d.shockMagnitute);
        yield return new WaitForSeconds(d.deathDelay);
        if(e == null) yield break;
        e.TakeDamage(d.damage, Enemy.DamageSource.PlayerSpell);
    }

    void TryCastFire()
    {
        if(isCasting) return;
        if(!SpellEconomy.Instance.TryCastElemental()) return;
        OnCastStarted?.Invoke();
        StartCoroutine(CastFire(fireSpellData));
    }

    IEnumerator CastFire(FireSpellData fire)
    {
        AudioManager.Instance.Play(AudioManager.Instance.spellCast);
        OnCastFire?.Invoke();
        ToastUI.Instance.Show("Casting FireSpell : Burning Enemies");
        JuiceManager.Instance.ScreenShake(0.5f,0.5f);
        var enemies = new List<Enemy>(EnemyManager.Instance.activeEnemies);
        float perTick = fire.totalDamage / (fire.burnDuration / fire.tickInterval);

        foreach (var e in enemies)
        {
            if(e == null) continue;
            e.ApplyBurn(perTick, fire.tickInterval, fire.burnDuration, fire.fireVFXPrefab);
            yield return new WaitForSeconds(Random.Range(0f, 0.05f));
        }
        yield return new WaitForSeconds(fire.burnDuration);
    }
    void TryCastEarth()
    {
        if(isCasting) return;
        if(!SpellEconomy.Instance.TryCastElemental()) return;
        OnCastStarted?.Invoke();
        StartCoroutine(CastEarth(earthSpellData));
    }

    IEnumerator CastEarth(EarthSpellData earth)
    {
        AudioManager.Instance.Play(AudioManager.Instance.spellCast);
        OnCastEarth?.Invoke();
        ToastUI.Instance.Show("Casting EarthSpell : 2X Damage On Enemies");
        JuiceManager.Instance.ScreenShake(0.5f,0.5f);
        var enemies = new List<Enemy>(EnemyManager.Instance.activeEnemies);
        foreach(var e in enemies)
        {
            if(e == null) continue;
            e.ApplyArmorBreak(earth.damageMultiplier, earth.duration, earth.vortexPrefab);
            yield return new WaitForSeconds(Random.Range(0f,0.05f));
        }
        yield return new WaitForSeconds(earth.duration);
    }

    void TryCastGalaxyTour()
    {
        if(isCasting) return;
        OnCastStarted?.Invoke();
        StartCoroutine(CastGalaxyTour(galaxyTourData));
    }

    IEnumerator CastGalaxyTour(GalaxyTourData data)
    {
        AudioManager.Instance.Play(AudioManager.Instance.spellCast);
        OnCastGalaxy?.Invoke();
        isCasting = true;

        var targets = new List<Enemy>(EnemyManager.Instance.activeEnemies);
        // foreach (var e in targets) e.Freeze();

        foreach(var e in targets)
        {
            Instantiate(data.portalPrefab, e.transform.position, Quaternion.identity);
            e.GalaxyDoom(data.sinkDuration, data.sinkDepth);
        }

        yield return new WaitForSeconds(data.sinkDuration);

        foreach (var e in targets)
           e.TakeDamage(data.damage, Enemy.DamageSource.ElementalSpell);
           JuiceManager.Instance.ScreenShake(0.5f,0.1f);
        
        EndingManager.Instance.StartEndingSequence();
    }


    
}
