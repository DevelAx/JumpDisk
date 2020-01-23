using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(HumanJump))]
public class HumanRun : MyMonoBehaviour
{
    [SerializeField]
    private HumanJump _jump = default;

    private float SpeedPerSecond => Settings.Human.RunSpeed;

    protected override void Awake()
    {
        base.Awake();
        Debug.Assert(_jump, nameof(_jump));
    }

    private void LateUpdate()
    {
        if (!_jump.IsGrounded)
            return;

        float updateSpeed = SpeedPerSecond * Time.smoothDeltaTime;
        transform.position += (Vector3.right * updateSpeed);
    }

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        _jump = _jump ?? this.RequireComponent<HumanJump>();
    }
}
