using UnityEngine;

[DisallowMultipleComponent]
public class Disk : SingleBehaviour<Disk>
{
    [SerializeField]
    [Range(1f, 5f)]
    private float _radius = 1.5f;

    public static float MinX => Instance.transform.position.x - Instance.transform.localScale.x / 2;

    private void OnValidate()
    {
        transform.localScale = new Vector3(_radius, transform.localScale.y, _radius);
    }
}
