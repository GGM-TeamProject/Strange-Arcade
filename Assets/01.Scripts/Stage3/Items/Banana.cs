using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Banana : Item
{   
    [SerializeField] private AudioClip _itemSound;
    private Stage3_CarInput _inputSystem;
    private ParticleSystem _stunParticle;

    protected override void Awake() {
        base.Awake(); 
        _inputSystem = _player.transform.GetComponent<Stage3_CarInput>();
        _stunParticle = _player.transform.Find("StunParticle").GetComponent<ParticleSystem>();
    }

    public override void OnUseItem()
    {
        GameManager.Instance.SoundManager.PlayerOneShot(_itemSound);
        _stunParticle.Play();
        _player.transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360);
        GameManager.Instance.ItemManager.BananaMethod(5f, _stunParticle, _inputSystem);
    }
}
