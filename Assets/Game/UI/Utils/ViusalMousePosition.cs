using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[DisallowMultipleComponent]
public class ViusalMousePosition : MyMonoBehaviour
{
    [SerializeField]
    private Text _text;

    private Vector3 _mousePos;

    protected override void Awake()
    {
        base.Awake();
        Debug.Assert(_text, nameof(_text));
    }

    private void Update()
    {
        UpdateMousePosition();
    }

    private void UpdateMousePosition()
    {
        if (_mousePos == Input.mousePosition)
            return;

        _mousePos = Input.mousePosition;
        _text.text = Input.mousePosition.ToString();
    }

    protected override void OnEditorValidate()
    {
        base.OnEditorValidate();
        _text = _text ?? this.RequireComponent<Text>();
    }
}
