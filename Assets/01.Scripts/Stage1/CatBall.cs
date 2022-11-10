using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBall : MonoBehaviour
{
    private Rigidbody2D _rigid;
    
    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector3 direct, float power){
        _rigid.AddForce(direct * power, ForceMode2D.Impulse);
    }
}
