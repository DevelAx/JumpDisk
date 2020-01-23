using UnityEngine;

[DisallowMultipleComponent]
public class DiskController : SingleBehaviour<DiskController>
{
    private int _diskId;

    public static float MinX => Instance.transform.position.x - Instance.transform.localScale.x / 2;

    protected override void Awake()
    {
        base.Awake();
        ResetDisk();
    }

    protected override void SubscribeToEvents()
    {
        base.SubscribeToEvents();
        this.SubscribeTo<Event_JumpCompleted>(OnJumpCompeted);
    }

    private void OnJumpCompeted(Event_JumpCompleted @event)
    {
        UpdateDisk();
    }

    private void UpdateDisk()
    {
        if (++_diskId >= Settings.Disks.Length)
            _diskId = 0;

        float radius = Settings.Disks[_diskId].Radius;
        transform.localScale = new Vector3(radius, transform.localScale.y, radius);
    }

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        ResetDisk();
    }

    private void ResetDisk()
    {
        _diskId = -1;
        UpdateDisk();
    }
}
