using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stage1_PlayerHP : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxHP = 5;

    private float _currentHP;

    public void OnDamage(float damage)
    {
        _currentHP -= damage;
        if(_currentHP <= 0){
            OnPlayerDie();
        }
    }

    private void OnPlayerDie(){
        Debug.Log("주금");
    }
}
