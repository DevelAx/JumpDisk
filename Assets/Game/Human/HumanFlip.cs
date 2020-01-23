using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HumanFlip : MyMonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        Debug.Assert(_animator != null, nameof(_animator));
    }

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        _animator = _animator ?? this.RequireComponent<Animator>();
    }

	#region Public

    public void Play(float speedRatio)
    {
        _animator.SetTrigger("Flip");
        _animator.speed = speedRatio;
    }

	#endregion
}
