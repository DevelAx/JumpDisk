using UnityEngine;

[DisallowMultipleComponent]
public class HumanPositionLimits : MyMonoBehaviour
{
    [SerializeField]
    private const float _xInvisibleShift = 30f;

    private void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x - Screen.width > _xInvisibleShift)
        {
            Vector3 originPos = Camera.main.ScreenToWorldPoint(new Vector3(-_xInvisibleShift, screenPos.y, screenPos.z));
            transform.position = new Vector3(originPos.x, transform.position.y, transform.position.z);
        }
    }
}
