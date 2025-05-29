using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    //public PlayerNavigator _playerNavigator;
    //private PlayerState prevState;

    private Animator _animator;
    public PlayerAnimationState playerAnimationState;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        //_playerNavigator = GetComponent<PlayerNavigator>();
        //prevState = PlayerState.Idle;
    }

    public void PlayIdleAnimation()
    {
        if (playerAnimationState != PlayerAnimationState.Idle)
        {
            playerAnimationState = PlayerAnimationState.Idle;
            _animator.SetTrigger("Idle");
        }
    }
    public void PlayRunAnimation(float angle)
    {
        if (playerAnimationState != PlayerAnimationState.Running)
        {
            playerAnimationState = PlayerAnimationState.Running;
            _animator.SetTrigger("Running");
        }
        _animator.SetFloat("WalkDirectionAngle", angle);
    }

    /*private void Update()
    {
        if (_playerNavigator.playerState == PlayerState.WalkingForwards && prevState != PlayerState.WalkingForwards)
        {
            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Walking Forwards");
        }
        if (_playerNavigator.playerState == PlayerState.WalkingBackwards && prevState != PlayerState.WalkingBackwards)
        {
            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Walking Backwards");
        }
        if (_playerNavigator.playerState == PlayerState.Idle && prevState != PlayerState.Idle)
        {
            _animator.SetTrigger("Idle");
        }
        //if (playerState == PlayerState.Jumping)
        //{
        //    _animator.SetTrigger("Jumping");
        //}

        prevState = _playerNavigator.playerState;
    }*/
}

public enum PlayerAnimationState
{
    Idle,
    Running,
}