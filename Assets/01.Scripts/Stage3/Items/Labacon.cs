using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labacon : Item 
{
    public override void OnUseItem()
    {
        player.PlayerSpeed /= 2.3f;
    }
}
