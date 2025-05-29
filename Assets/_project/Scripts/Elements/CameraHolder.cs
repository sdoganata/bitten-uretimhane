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

    private void Start()
    {
        _cameraStartPos = mainCamera.transform.localPosition;
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
