using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RollObject : MonoBehaviour
{
    [SerializeField] private float _rollCoolTime = 30f;
    [SerializeField] private float _rollCoolDownValue = 5f;

    private bool _isRoll = false;
    public bool IsRoll => _isRoll;

    [SerializeField] private Stage2_Cat _cat;
    [SerializeField] private Transform _rollObjectParent;
    private Queue<Vector3> _bulletVelocitys = new Queue<Vector3>();

    public void Init(){
        _rollCoolTime = 30f;
        StopAllCoroutines();
        StartCoroutine(TurnCoroutine());
    }

    private void FixedUpdate() {
        if(!_isRoll){
            _bulletVelocitys.Clear();
            foreach(Stage2_Bullet bullet in GameManager.Instance.BulletStore.GetComponentsInChildren<Stage2_Bullet>()){
                _bulletVelocitys.Enqueue(bullet.LastVelocity);
            }
        }
    }

    IEnumerator TurnCoroutine(){
        while(_cat.CatState != CatState.Die){
            yield return new WaitForSeconds(_rollCoolTime);
            TurnObject();
        }
    }

    private void TurnObject(){
        _isRoll = true;
        float lateRotate = Mathf.Round(transform.rotation.eulerAngles.z);
        foreach(Stage2_Bullet bullet in GameManager.Instance.BulletStore.GetComponentsInChildren<Stage2_Bullet>()){
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        Sequence sq = DOTween.Sequence();
        sq.Append(_rollObjectParent.DORotate(new Vector3(0, 0, lateRotate + 10), 0.5f));
        sq.Append(_rollObjectParent.DORotate(new Vector3(0, 0, lateRotate - 90), 0.3f));
        sq.OnComplete(() => {
            _isRoll = false;
            foreach(Stage2_Bullet bullet in GameManager.Instance.BulletStore.GetComponentsInChildren<Stage2_Bullet>()){
                bullet.GetComponent<Rigidbody2D>().velocity = _bulletVelocitys.Dequeue();
            }
            _rollCoolTime -= _rollCoolDownValue;
            _rollCoolTime = Mathf.Clamp(_rollCoolTime, 5, 30);
        });
    }
}
