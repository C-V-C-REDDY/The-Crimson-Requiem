using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private player playerScript;
    [SerializeField] ElementalCaster elementalCaster;

    void OnEnable()
    {
        elementalCaster.OnCastStarted += PlayCastAnim;
    }
    void OnDisable()
    {
        elementalCaster.OnCastStarted -= PlayCastAnim;
    }

    void PlayCastAnim()
    {
        animator.ResetTrigger("CastAnim");
        animator.SetTrigger("CastAnim");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        animator.SetBool("IsMoving", playerScript.GetIsMoving());
    }

}
