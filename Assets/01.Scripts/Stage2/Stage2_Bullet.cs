using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigid;
    private RollObject _rollObj;
    private Vector2 _lastVelocity = default(Vector2);
    public Vector2 LastVelocity => _lastVelocity;
    
    private int _destroyCnt = 3;

    private void Awake() {
        _rollObj = transform.parent.parent.GetComponent<RollObject>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        _destroyCnt = 3;
    }

    private void Update() {
        _lastVelocity = _rigid.velocity;
    }

    public void SetDirection(Vector3 direction){
        _rigid.velocity = direction * _speed;
    }

    public void OnKill(){
        GameObject particle = PoolManager.Instance.Pop("BulletKillParticle");
        particle.transform.position = transform.position;
        PoolManager.Instance.Push(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //이거 물어보기
        if(other.transform.CompareTag("Player") || other.transform.CompareTag("Border")){
            _destroyCnt--;
            if(_destroyCnt <= 0){
                OnKill();
            }

            Vector2 dir = Vector2.Reflect(_lastVelocity.normalized, other.contacts[0].normal);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            
            _rigid.velocity = dir * Mathf.Max(_speed, 0);
        }

        if(other.transform.CompareTag("Stage2_Cat")){
            //고양이 데미지 추가하기
            OnKill();
        }
    }
}
