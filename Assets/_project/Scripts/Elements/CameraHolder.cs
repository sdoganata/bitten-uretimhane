using DG.Tweening;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;
    public float offsetByLookDirection;
    public float smoothTime;
    private Vector3 _vel;

    public Camera mainCamera;
    private Vector3 _cameraStartPos;
    
    public Player player;
    public LayerMask groundLayerMask;
    private Transform _playerTransform;
    private Transform _mainCameraTransform;

    private void Start()
    {
        _cameraStartPos = mainCamera.transform.localPosition;
        _playerTransform = player.transform;
        _mainCameraTransform = mainCamera.transform;
    }

    private void Update()
    {
        var distanceVector = _playerTransform.position - _mainCameraTransform.position;
        if (Physics.Raycast(_mainCameraTransform.position,
            distanceVector, out var hit, distanceVector.magnitude, groundLayerMask))
        {
            //material color reduce alpha
        }
        if (hit.transform == null)
        {
            //do this for left and right of the player and then if one of them hits but player's doesn't
            //make object colored again

        }

    }

    private void FixedUpdate()
    {
        var targetPos = followObject.position + followObject.forward*offsetByLookDirection;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _vel, smoothTime);
    }

    /*public void ChangeCameraTarget(Transform tr)
    {
        followObject = tr;
    }*/

    public void ShakeCamera(float magnitude, float duration)
    {
        mainCamera.transform.DOKill();
        mainCamera.transform.localPosition = _cameraStartPos;
        mainCamera.transform.DOShakePosition(magnitude, duration);
    }
}
