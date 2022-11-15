using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RollObject : MonoBehaviour
{
    private bool _isRoll = false;
    public bool IsRoll => _isRoll;

    private Transform _rollObjectParent;
    private Transform _bulletStore;

    private void Awake() {
        _rollObjectParent = GameObject.Find("Screen/Stages/Stage_2/RollObjects").transform;
    }

    [ContextMenu("test")]
    public void TurnObject(){
        _isRoll = true;
        float lateRotate = (int)transform.rotation.z;
        Sequence sq = DOTween.Sequence();
        sq.Append(_rollObjectParent.DORotate(new Vector3(0, 0, lateRotate - 10), 0.5f));
        sq.Append(_rollObjectParent.DORotate(new Vector3(0, 0, lateRotate + 90), 0.3f));
        sq.OnComplete(() => _isRoll = false);
    }
}
