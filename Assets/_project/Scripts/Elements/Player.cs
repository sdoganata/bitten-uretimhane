using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    private PlayerNavigator _playerNavigator;
    private Animator _animator;
    public PlayerState playerState;
    //private PlayerState prevState;

    public int startHealth;
    private int _currentHealth;

    public GameObject interactingObject;
    public float touchDistance;
    public LayerMask interactableLayerMask;

    private bool _haveKey;

    public Weapon weapon;
    private void Awake()
    {
        _playerNavigator = GetComponent<PlayerNavigator>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {

        _currentHealth = startHealth;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * touchDistance);
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out var hit, touchDistance, interactableLayerMask))
        {
            interactingObject = hit.transform.gameObject;
        }
        else
        {
            interactingObject = null;
        }

        if (Input.GetKeyDown(KeyCode.E) && interactingObject != null)
        {
            ExecuteInteractAction();
        }
    
    }

    private void ExecuteInteractAction()
    {
        var door = interactingObject.GetComponent<Door>();
        if (door != null) 
        {
            door.DoorInteracted(_haveKey);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.
                gameObject);
            _haveKey = true;
        }
        if (other.CompareTag("WeaponCollectable"))
        {
            gameDirector.inventoryUI.WeaponCollected(other.GetComponent<WeaponCollectable>().weaponType);
            other.gameObject.SetActive(false);
        }
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
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        gameObject.SetActive(true);
        _playerNavigator.ResetPosition();
        playerState = PlayerState.Alive;
        //_currentHealth = startHealth;
        //gameDirector.playerHealthUI.UpdateHealth(1);
    }

    internal void GetHit()
    {
        _currentHealth -= 1;
        if (_currentHealth <= 0 && playerState != PlayerState.Dead) {
            Die();
        }

        gameDirector.audioManager.PlayGetHitSFX();
        gameDirector.cameraHolder.ShakeCamera(.5f, .5f);
        gameDirector.playerHealthUI.UpdateHealth((float)_currentHealth / startHealth);
        gameDirector.playerHitUI.PopPlayerHitUI();

    }

    private void Die()
    {
        playerState = PlayerState.Dead;
    }

    public void UseKey()
    {
        _haveKey = false;
    }
}

public enum PlayerState
{
    Dead,
    Alive
}
