using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _boundPower;
    [SerializeField] private float _speed;
    
    private Rigidbody _rigid;
    private Vector3 _lastVelocity;
    private Transform _targetTrm;

    private void Awake() {
        _targetTrm = GameObject.Find("Player").transform;
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start() {
        _rigid.velocity = (_targetTrm.position - transform.position).normalized * _speed;
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
