using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Banana : MonoBehaviour, Iitem
{   
    private Stage3_Car _player;
    private Stage3_CarInput _inputSystem;
    private ParticleSystem _stunParticle;

    private void Awake() {
        _player = GameObject.Find("Screen/Stages/Stage_3/PlayerCar").GetComponent<Stage3_Car>();    
        _inputSystem = _player.transform.GetComponent<Stage3_CarInput>();
        _stunParticle = _player.transform.Find("StunParticle").GetComponent<ParticleSystem>();
    }

    public void OnUseItem()
    {
        _stunParticle.Play();
        _player.transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360);
        _inputSystem._isMirror = true;
        Invoke("CallBack", 5f);
    }

    private void CallBack(){
        _stunParticle.Stop();
        _inputSystem._isMirror = false;
    }
}
