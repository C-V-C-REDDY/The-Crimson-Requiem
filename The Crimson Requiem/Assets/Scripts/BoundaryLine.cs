using Unity.Mathematics;
using UnityEngine;

public class BoundaryLine : MonoBehaviour
{
    public static BoundaryLine Instance { get; private set; }

    public float maxHP = 100f;
    [SerializeField] private float currentHP;
    public bool IsBroken { get; private set; } = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        if (IsBroken) return;

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
        Debug.Log("Boundary Line took " + amount + " damage. Current HP: " + currentHP);

        if(currentHP <= 0f)
        {
            Break();
        }
    }

    void Break()
    {
        IsBroken = true;
        GetComponent<Collider>().enabled = false;
        // Add any additional logic for when the boundary line is broken
    }
}
