using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;
    public float offsetByLookDirection;
    public float smoothTime;
    private Vector3 _vel;
    private void Update()
    {
        var targetPos = followObject.position + followObject.forward*offsetByLookDirection;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _vel, smoothTime);
    }

    /*public void ChangeCameraTarget(Transform tr)
    {
        followObject = tr;
    }*/
}
