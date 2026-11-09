
/// <summary>
/// WHAT: Drives all of Yami's animations: idle/run from isMoving, the death trigger, and the
///       cast animation.
/// USED BY: Animator on Yami.
/// DEPENDS ON: Player.cs (isMoving), PlayerManager.OnDied (death), ElementalCaster's cast
///             event for "CastAnim".
/// NOTE: Keeps animation separate from movement so either can change without touching the other.
/// </summary>

using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private player playerScript;
    [SerializeField] ElementalCaster elementalCaster;

    PlayerManager playerManager;


    public void Start()
    {
        playerManager = PlayerManager.Instance;
        playerManager.OnDied += DeathAnim;
    }

    public void ODestroy()
    {
        playerManager.OnDied -= DeathAnim;
    }


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
        animator.ResetTrigger("Dead");

    }

    private void Update()
    {
        animator.SetBool("IsMoving", playerScript.GetIsMoving());
        animator.speed = PlayerManager.speedMultiplier;

    }


    private void DeathAnim()
    {
        animator.ResetTrigger("Dead");
        animator.SetTrigger("Dead");
    }

}
