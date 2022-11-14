using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigid;
    private Vector2 _lastVelocity = default(Vector2);

    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _rigid.velocity = new Vector2(1, 1) * _speed;
    }

    private void Update() {
        _lastVelocity = _rigid.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //이거 물어보기
        if(other.transform.CompareTag("Player") || other.transform.CompareTag("Border")){
            Vector2 dir = Vector2.Reflect(_lastVelocity.normalized, other.contacts[0].normal);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            _rigid.velocity = dir * Mathf.Max(_speed, 0);
        }
    }
}
