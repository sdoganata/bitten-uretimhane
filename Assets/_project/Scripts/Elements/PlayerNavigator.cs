using System;
using UnityEngine;

public class PlayerNavigator : MonoBehaviour
{

    public float speed;
    public float jumpPower;
    private Rigidbody _rb;
    private Transform _tr;

    public bool playerLooksAtMouse;
    private bool _isOnGround;
    public LayerMask lookAtLayerMask;

    public PlayerState playerState;


    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MovePlayerWithKeys();

            if (playerLooksAtMouse)
            {
                LookAtMouse();
            }
        
    }

    private void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 50, lookAtLayerMask))
        {
            //print(hit.collider.gameObject.name);
            //print(hit.point);
            var lookPos = hit.point;
            lookPos.y = _tr.position.y;
            _tr.LookAt(lookPos);
        }
        //Debug.DrawRay(ray.origin, ray.direction * 100)
    }

    void MovePlayerWithKeys()
    {
        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
            playerState = PlayerState.WalkingForwards;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
            playerState = PlayerState.WalkingBackwards;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
            playerState = PlayerState.WalkingForwards;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
            playerState = PlayerState.WalkingForwards;
        }

        var yVelocity = _rb.linearVelocity;

        yVelocity.x = 0;
        yVelocity.z = 0;

        _rb.linearVelocity = direction.normalized * speed + yVelocity;

        _isOnGround = Physics.Raycast(_tr.position + Vector3.up * 0.1f, Vector3.down, 1, lookAtLayerMask);

        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpPower, _rb.linearVelocity.z);
            //playerState = PlayerState.Jumping;
        }

    }

    public void ResetPosition()
    {
        _rb.position = Vector3.zero;
    }
}

public enum PlayerState
{
    Idle,
    WalkingForwards,
    WalkingBackwards,
    Jumping,
    Dead,
}
