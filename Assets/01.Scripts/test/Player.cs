using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigid;

    private void Awake() {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, y, 0);

        _rigid.velocity = moveDir.normalized * _speed;
    }
}
