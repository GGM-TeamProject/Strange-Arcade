using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Banana : Item
{   
    private Stage3_CarInput _inputSystem;
    private ParticleSystem _stunParticle;

    protected override void Awake() {
        base.Awake(); 
        _inputSystem = player.transform.GetComponent<Stage3_CarInput>();
        _stunParticle = player.transform.Find("StunParticle").GetComponent<ParticleSystem>();
    }

    public override void OnUseItem()
    {
        _stunParticle.Play();
        player.transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360);
        GameManager.Instance.ItemManager.BananaMethod(5f, _stunParticle, _inputSystem);
        PoolManager.Instance.Push(gameObject);
    }
}
