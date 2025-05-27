using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public PlayerState playerState;
    private Animator _animator;
    private PlayerState prevState;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        prevState = PlayerState.Idle;
    }

    private void Update()
    {
        if (playerState == PlayerState.WalkingForwards && prevState != PlayerState.WalkingForwards)
        {
            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Walking Forwards");
        }
        if (playerState == PlayerState.WalkingBackwards && prevState != PlayerState.WalkingBackwards)
        {
            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Walking Backwards");
        }
        if (playerState == PlayerState.Idle && prevState != PlayerState.Idle)
        {
            _animator.SetTrigger("Idle");
        }
        /*if (playerState == PlayerState.Jumping)
        {
            _animator.SetTrigger("Jumping");
        }*/

        prevState = playerState;
    }
}
