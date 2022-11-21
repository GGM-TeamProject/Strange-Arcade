using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labacon : MonoBehaviour, Iitem 
{
    private Stage3_Car _player;
    
    private void Awake() {
        _player = GameObject.Find("Screen/Stages/Stage_3/PlayerCar").GetComponent<Stage3_Car>();
    }

    public void OnUseItem()
    {
        IDamage damage = _player.GetComponent<IDamage>();
        damage?.OnDamage(1f);
    }
}
