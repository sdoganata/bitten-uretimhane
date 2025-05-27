using UnityEngine;

public class PickUpInputController : MonoBehaviour
{
    public LayerMask pickUpLayerMask;
    public LayerMask groundLayerMask;

    private GameObject _pickedUpObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 50, pickUpLayerMask))
            {
                _pickedUpObject = hit.collider.gameObject;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 50, groundLayerMask))
            {
                _pickedUpObject.transform.position = hit.point + Vector3.up;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 50, groundLayerMask))
            {
                _pickedUpObject.transform.position = hit.point;
            }
        }
    }
}
