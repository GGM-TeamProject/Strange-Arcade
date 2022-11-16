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

    private Transform _rollObjectParent;
    private Transform _bulletStore;

    private void Awake() {
        _rollObjectParent = GameObject.Find("Screen/Stages/Stage_2/RollObjects").transform;
    }

    private void Start() {
        StartCoroutine(TurnCoroutine());
    }

    IEnumerator TurnCoroutine(){
        while(true){
            TurnObject();
            yield return new WaitForSeconds(_rollCoolTime);
        }
    }

    private void TurnObject(){
        _isRoll = true;
        float lateRotate = Mathf.Round(transform.rotation.eulerAngles.z);
        Sequence sq = DOTween.Sequence();
        sq.Append(_rollObjectParent.DORotate(new Vector3(0, 0, lateRotate + 10), 0.5f));
        sq.Append(_rollObjectParent.DORotate(new Vector3(0, 0, lateRotate - 90), 0.3f));
        sq.OnComplete(() => {
            _isRoll = false;
            _rollCoolTime -= _rollCoolDownValue;
        });
    }
}
