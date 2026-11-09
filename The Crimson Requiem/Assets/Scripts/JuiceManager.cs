


/// <summary>
/// WHAT: Generic feedback triggers (ScreenShake, hit-stop) via a Cinemachine impulse source.
/// USED BY: DarkOrbProjectile, ElementalCaster, hazards (verify).
/// </summary>



using UnityEngine;
using System.Collections;
using Unity.Cinemachine;


public class JuiceManager : MonoBehaviour
{
    public static JuiceManager Instance;
    public CinemachineImpulseSource impulseSource;

    void Awake()
    {
        if(Instance != null && Instance != this) { Destroy(gameObject); return;}
        Instance = this;
    }

    public void Hitstop(float duration)
    {
        StartCoroutine(DoHitStop(duration));
    }

    IEnumerator DoHitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    public void ScreenShake(float force, float duration)
    {
        StartCoroutine(ShakeRoutine(force, duration));
    }
    IEnumerator ShakeRoutine(float force, float duration)
    {
        impulseSource.ImpulseDefinition.ImpulseDuration = duration;
        impulseSource.GenerateImpulseWithForce(force);
        yield break;
    }

}
