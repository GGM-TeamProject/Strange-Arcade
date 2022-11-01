using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stage1_JumpGauge : MonoBehaviour
{
    private Transform _valueTrm;
    public Transform ValueTrm {get => _valueTrm; set => _valueTrm = value;}

    private void Awake() {
        _valueTrm = transform.Find("BackGround/Value");
    }

    public void SetJumpGauge(float value){
        _valueTrm.localScale = new Vector3(1, Mathf.Lerp(0, 1, (value - 3) / 10 * 2), 1);
    }
}
