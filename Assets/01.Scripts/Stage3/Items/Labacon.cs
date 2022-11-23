using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labacon : Item 
{
    private Stage3_Car _player;
    
    private void Awake() {
        _player = GameObject.Find("Screen/Stages/Stage_3/PlayerCar").GetComponent<Stage3_Car>();
    }

    public override void OnUseItem()
    {
        IDamage damage = _player.GetComponent<IDamage>();
        damage?.OnDamage(1f);
        PoolManager.Instance.Push(gameObject);
    }
}
