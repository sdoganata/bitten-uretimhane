using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerNavigator _playerNavigator;
    private Animator _animator;
    public PlayerState playerState;
    //private PlayerState prevState;

    private void Awake()
    {
        _playerNavigator = GetComponent<PlayerNavigator>();
        _animator = GetComponentInChildren<Animator>();
    }

    /*private void Update()
    {
        if (playerState == PlayerState.WalkingForwards && prevState != PlayerState.WalkingForwards)
        {
            _animator.SetTrigger("Walking Forwards");
        }
        if (playerState == PlayerState.WalkingBackwards && prevState != PlayerState.WalkingBackwards)
        {
            _animator.SetTrigger("Walking Backwards");
        }
        if (playerState == PlayerState.Jumping)
        {
            _animator.SetTrigger("Jumping");
        }

        prevState = playerState;
    }*/

    internal void RestartPlayer()
    {
        gameObject.SetActive(true);
        _playerNavigator.ResetPosition();
        playerState = PlayerState.Idle;
    }

    internal void GetHit()
    {
        if (playerState != PlayerState.Dead)
        {
        //gameObject.SetActive(false);
        playerState = PlayerState.Dead;
            _animator.SetTrigger("Die");
        }
    }
}
