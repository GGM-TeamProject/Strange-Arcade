using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Stage1_PlayerHP : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxHP = 5;

    private float _currentHP;
    private ParticleSystem _playerDieParticle;

    private void Awake() {
        _playerDieParticle = transform.Find("PlayerDieParticle").GetComponent<ParticleSystem>();
    }

    private void Start() {
        _currentHP = _maxHP;
    }

    public void OnDamage(float damage, UnityEvent CallBack = null)
    {
        _currentHP -= damage;
        if(_currentHP <= 0){
            OnPlayerDie(CallBack);
        }
    }

    private void OnPlayerDie(UnityEvent CallBack){
        Debug.Log("주금");
        _playerDieParticle.Play();
        StartCoroutine(PlayerDieCoroutine(CallBack));
    }

    IEnumerator PlayerDieCoroutine(UnityEvent CallBack){
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameOverPanel();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R)); //나중에 수정
        _currentHP = _maxHP;
        CallBack?.Invoke();
        GameManager.Instance.UIManager.OffGameOverPanel();
    }
}
