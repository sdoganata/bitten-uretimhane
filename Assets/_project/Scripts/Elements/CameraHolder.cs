using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;
    private void Update()
    {
        transform.position = followObject.position;
    }

    /*public void ChangeCameraTarget(Transform tr)
    {
        followObject = tr;
    }*/
}
