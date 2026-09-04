using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [Header("SetUp")]
    public Transform firePoint;
    public SpellData darkOrbData;

    private DarkOrbEffect darkOrbEffect;
    private float lastCastTime;
    public Animator animator;

    public PlayerAnimation playerAnimation; // Reference to the PlayerAnimation script

    void Awake()
    {
        darkOrbEffect = GetComponent<DarkOrbEffect>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryCastDarkOrb();
        }
    }

    void TryCastDarkOrb()
    {
        if (Time.time < lastCastTime + darkOrbData.cooldown)
        {
            return; // Still in cooldown
        }

        lastCastTime = Time.time;


        Vector3 direction = GetAimDirection();
        darkOrbEffect.Cast(firePoint.position, direction, darkOrbData);
    }

    Vector3 GetAimDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 targetPoint = hit.point;
            targetPoint.y = firePoint.position.y; // Keep the y-coordinate the same as the fire point
            return (targetPoint - firePoint.position).normalized;
        }
        else
        {
            return ray.direction; // Default direction if nothing is hit
        }
    }
}
