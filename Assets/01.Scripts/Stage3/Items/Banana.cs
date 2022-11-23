using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Banana : Item
{   
    private Stage3_Car _player;
    private Stage3_CarInput _inputSystem;
    private ParticleSystem _stunParticle;

    private void Awake() {
        _player = GameObject.Find("Screen/Stages/Stage_3/PlayerCar").GetComponent<Stage3_Car>();    
        _inputSystem = _player.transform.GetComponent<Stage3_CarInput>();
        _stunParticle = _player.transform.Find("StunParticle").GetComponent<ParticleSystem>();
    }

    public override void OnUseItem()
    {
        _stunParticle.Play();
        _player.transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360);
        GameManager.Instance.ItemManager.BananaMethod(5f, _stunParticle, _inputSystem);
        PoolManager.Instance.Push(gameObject);
    }

    
}
