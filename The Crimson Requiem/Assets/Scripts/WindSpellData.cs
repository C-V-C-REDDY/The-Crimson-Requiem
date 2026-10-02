using UnityEngine;

[CreateAssetMenu(fileName = "WindSpellData", menuName = "Spells/Wind")]

public class WindSpellData : ElementalSpellData
{
    [Header("Tornado")]
    public GameObject tornadoPrefab;
    public Vector3 tornadoPosition = new Vector3(5f, 0, 30f);
    public float pullInDuration = 1f;
    public float pullInSpeed = 8f;
    public float captureRadius = 8f;

    [Header("Orbit")]
    public float orbitDuration = 3f;
    public float orbitSpeed = 3f;
    public float orbitRadius = 3f;
    public float liftHeight = 2f;

    [Header("Fling")]
    public float flingDistance = 8f;
    public float flingDuration = 0.5f;
    public float flingSpeed = 16f;
}
