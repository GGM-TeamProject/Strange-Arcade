using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labacon : Item 
{
    public override void OnUseItem()
    {
        IDamage damage = player.GetComponent<IDamage>();
        damage?.OnDamage(1f);
        PoolManager.Instance.Push(gameObject);
    }
}
