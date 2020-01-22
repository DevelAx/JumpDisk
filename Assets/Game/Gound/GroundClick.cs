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
        Vector3 mousePos = Vector3.forward * (-Camera.main.transform.position.z) + Input.mousePosition;
        Vector3 worldClickPoint = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 targetPosition = Vector3.right * worldClickPoint.x;
        new Event_JumpPressed(targetPosition);
    }
}
