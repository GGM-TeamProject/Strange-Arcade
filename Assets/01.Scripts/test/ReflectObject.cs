using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectObject : MonoBehaviour
{
    private float _boundPower;
    
    private Rigidbody _rigid;
    private Vector3 _lastVelocity;

    private void Awake() {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start() {
        _rigid.velocity = new Vector3(5, 0, 0);
    }

    private void Update() {
        _lastVelocity = _rigid.velocity;
    }

    private void OnCollisionEnter(Collision other) {
        _boundPower = _lastVelocity.magnitude;
        Vector3 boundDir = Vector3.Reflect(_lastVelocity.normalized, other.contacts[0].normal);

        _rigid.velocity = boundDir * _boundPower;
    }
}
