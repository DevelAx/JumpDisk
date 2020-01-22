using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    private float _speedPerSecond;
    private Rigidbody _body;

    private void Awake()
    {
        _speedPerSecond = Game.Settings.Human.RunSpeed;
        _body = this.RequireComponent<Rigidbody>();
    }

    private void Update()
    {
        float updateSpeed = _speedPerSecond * Time.deltaTime;
        transform.position += (Vector3.right * updateSpeed);
    }

    //private void FixedUpdate()
    //{
    //    float updateSpeed = _speedPerSecond * Time.fixedDeltaTime;
    //    _body.MovePosition(_body.position + Vector3.right * updateSpeed);
    //}
}
