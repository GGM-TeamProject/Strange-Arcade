using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Stage1_PlayerHP : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxHP = 5;
    [SerializeField] private AudioClip _dieSound;

    private float _currentHP;
    private Player_Stage1 _player;
    private ParticleSystem _playerDieParticle;

    private void Awake() {
        _player = GetComponent<Player_Stage1>();
        _playerDieParticle = transform.Find("PlayerDieParticle").GetComponent<ParticleSystem>();
    }

    private void Start() {
        _currentHP = _maxHP;
    }

    public void OnDamage(float damage, UnityEvent CallBack = null)
    {
        _currentHP -= damage;
        if(_currentHP <= 0 && _player.PlayerEnum != PlayerEnum.Die && !SceneTransManager.Instance.IsChangeScene &&
            !GameManager.Instance.UIManager.IsGameClear && !GameManager.Instance.UIManager.IsGameOver){
            _player.PlayerEnum = PlayerEnum.Die;
            OnPlayerDie(CallBack);
        }
    }

    private void OnPlayerDie(UnityEvent CallBack){
        Debug.Log("주금");
        GameManager.Instance.SoundManager.PlayerOneShot(_dieSound);
        GameManager.Instance.ChallengeManager.CheckClear("FirstDeath_S1");
        _playerDieParticle.Play();
        StartCoroutine(PlayerDieCoroutine(CallBack));
    }

    IEnumerator PlayerDieCoroutine(UnityEvent CallBack){
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameOverPanel(false);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R)); //나중에 수정
        _currentHP = _maxHP;
        CallBack?.Invoke();
        GameManager.Instance.UIManager.OffGameOverPanel(false);
    }
}
