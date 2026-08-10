using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private player playerScript;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        animator.SetBool("IsMoving", playerScript.GetIsMoving());
    }
}
