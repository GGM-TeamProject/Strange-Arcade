using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labacon : Item 
{
    [SerializeField] private AudioClip _itemSound;

    public override void OnUseItem()
    {
        GameManager.Instance.SoundManager.PlayerOneShot(_itemSound);
        player.PlayerSpeed /= 2.3f;
    }
}
