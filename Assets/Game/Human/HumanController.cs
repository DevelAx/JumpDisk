using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(HumanJump), typeof(HumanRun))]
public class HumanController : MyMonoBehaviour
{
    [SerializeField]
    private HumanJump _jump = default;

    private bool _onDisk;

    private bool CanJump => _jump.IsGrounded && _onDisk;

    protected override void Awake()
    {
        base.Awake();
        Debug.Assert(_jump, nameof(_jump));
    }

    private void OnJumpPressed(Event_JumpPressed @event)
    {
        if (CanJump && IsTargetCorrect(@event.Position))
        {
            _jump.JumpTo(@event.Position);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (IsJumpDisk(collider))
            _onDisk = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (IsJumpDisk(collider))
            _onDisk = false;
    }

    protected override void SubscribeToEvents()
    {
        base.SubscribeToEvents();
        Events.SubscribeTo<Event_JumpPressed>(OnJumpPressed);
    }

    protected override void UnsubscribeFromEvents()
    {
        base.UnsubscribeFromEvents();
        Events.UnsubscribeFrom<Event_JumpPressed>(OnJumpPressed);
    }

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        _jump = _jump ?? this.RequireComponent<HumanJump>();
    }

    #region Helpers

    private bool IsJumpDisk(Collider collider)
    {
        return collider.tag == Constants.Tags.Disk;
    }

    private bool IsTargetCorrect(Vector3 position)
    {
        return IsTargetBehindPlayer(position) && IsTargetBehindDisk(position);
    }

    private bool IsTargetBehindPlayer(Vector3 position)
    {
        return transform.position.x > position.x;
    }

    private bool IsTargetBehindDisk(Vector3 position)
    {
        return Disk.MinX > position.x;
    }

    #endregion
}
