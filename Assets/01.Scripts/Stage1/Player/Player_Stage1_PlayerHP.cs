using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stage1_PlayerHP : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxHP = 5;

    private float _currentHP;
    private ParticleSystem _playerHitParticle;

    private void Awake() {
        _playerHitParticle = transform.Find("PlayerHitParticle").GetComponent<ParticleSystem>();
    }

    private void Start() {
        _currentHP = _maxHP;
    }

    public void OnDamage(float damage, Action CallBack = null)
    {
        _currentHP -= damage;
        if(_currentHP <= 0){
            OnPlayerDie(CallBack);
        }
    }

    private void OnPlayerDie(Action CallBack){
        Debug.Log("주금");
        _playerHitParticle.Play();
        StartCoroutine(PlayerDieCoroutine(CallBack));
    }

    IEnumerator PlayerDieCoroutine(Action CallBack){
        GetComponent<Player_Stage1>().PlayerEnum = PlayerEnum.Die;

        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameOverPanel();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R)); //나중에 수정
        CallBack?.Invoke();
        GameManager.Instance.UIManager.OffGameOverPanel();
    }
}
