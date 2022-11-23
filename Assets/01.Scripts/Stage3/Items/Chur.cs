using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chur : Item
{
    private List<SpriteRenderer> _churBombs = new List<SpriteRenderer>();

    private void OnEnable() {
        _churBombs.Clear();
        foreach(Transform _churBomb in GameObject.Find("Screen/Stages/Stage_3/ChurBomb").transform){
            _churBombs.Add(_churBomb.GetComponent<SpriteRenderer>());
        }
    }

    public override void OnUseItem()
    {
        if(GameManager.Instance.ItemManager.AttackRoutineIsRunning) return;
        GameManager.Instance.ItemManager.ChurMehod(_churBombs);
        PoolManager.Instance.Push(gameObject);
    }
}
