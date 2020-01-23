using UnityEngine;

[DisallowMultipleComponent]
public class GroundClick : MyMonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnMouseDown()
    {
        Vector3 screenPoint = Vector3.forward * (-Camera.main.transform.position.z) + Input.mousePosition;
        Vector3 worldClickPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        Vector3 worldPoint = Vector3.right * worldClickPoint.x;
        new Event_JumpPressed(screenPoint, worldPoint);
    }
}
