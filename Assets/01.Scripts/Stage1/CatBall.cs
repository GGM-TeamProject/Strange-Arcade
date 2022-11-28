using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBall : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private Player_Stage1 _player;
    
    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Screen/Stages/Stage_1/Player").GetComponent<Player_Stage1>();
    }

    private void Update() {
        //if(_player.PlayerEnum == PlayerEnum.Die || !_player.gameObject.activeSelf || _rigid.velocity == Vector2.zero) PoolManager.Instance.Push(gameObject);
    }

    public void Movement(Vector3 direct, float power){
        _rigid.AddForce(direct * power, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("DestroyZone")){
            //PoolManager.Instance.Push(gameObject);
        }
    }
}
