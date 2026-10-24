using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    [SerializeField] Image vignetee;

    [Header("Tuning")]
    [SerializeField] float startThreshold = 0.7f;
    [SerializeField] float maxAlpha = 0.85f;
    [SerializeField] float smoothSpeed = 4f;

    [Header("Low HP pulse")]
    [SerializeField] float pulseThreshold = 0.3f;
    [SerializeField] float pulseSpeed = 4f;
    [SerializeField] float pulseAmount = 0.1f;

    float shieldPercent = 1f;
    float targetAlpha;
    float currentAlpha;

    private BoundaryLine boundaryLine;

    public void Start()
    {
        boundaryLine = BoundaryLine.Instance;
        boundaryLine.OnBoundaryHPChange += SetShieldPercent;
    }
    public void OnDestroy()
    {
        boundaryLine.OnBoundaryHPChange -= SetShieldPercent;
    }


    public void SetShieldPercent(float current, float max)
    {
        float percent = current / max;
        shieldPercent = Mathf.Clamp01(percent);
        float t = Mathf.InverseLerp(startThreshold, 0f, shieldPercent);
        targetAlpha = t * maxAlpha;
    }

    void Update()
    {
        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, smoothSpeed * Time.deltaTime);

        float a = currentAlpha;
        if(shieldPercent <= pulseThreshold)
             a += Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        
        Color c = vignetee.color;
        c.a = Mathf.Clamp01(a);
        vignetee.color = c;
    }
}
