using UnityEngine;
using System.Collections;
using Unity.Cinemachine;


public class JuiceManager : MonoBehaviour
{
    public static JuiceManager Instance;
    public CinemachineImpulseSource impulseSource;

    void Awake()
    {
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

    public void ScreenShake(float force)
    {
        impulseSource.GenerateImpulse(force);
    }

}
