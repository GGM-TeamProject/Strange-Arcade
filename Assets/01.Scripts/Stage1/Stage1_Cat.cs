using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Cat : MonoBehaviour
{
    [SerializeField] private float _summonDelay = 5f;

    private Player_Stage1 _player;
    private Transform _spriteTrm;
    private Animator _anim;

    private const float _summonMinusTime = 30f;
    private const float _summonDelayMin = 2f;
    private float _currentTime = 0;

    private void Awake() {
        _spriteTrm = transform.Find("Sprite");
        _anim = _spriteTrm.GetComponent<Animator>();
        _player = transform.parent.Find("Player").GetComponent<Player_Stage1>();
    }

    private void Start() {
        Init();
    }

    private void Update() {
        SpawnTimeTimer();
    }

    public void Init(){
        StopAllCoroutines();
        StartCoroutine(UpdatePath());
    }

    private void SpawnTimeTimer(){
        _currentTime += Time.deltaTime;

        if(_currentTime >= _summonDelayMin){
            _summonDelay--;
            Mathf.Clamp(_summonDelay, _summonDelayMin, 5);
            _currentTime = 0;
        }
    }

    IEnumerator UpdatePath(){
        while(_player.PlayerEnum != PlayerEnum.Die){
            int randomValue = Random.Range(0, 2);

            Vector2 spawnPos = (randomValue == 0) ? Vector3.right : Vector3.left;
            _spriteTrm.localScale = new Vector3(spawnPos.x, 1, 1);

            GameObject catBall = PoolManager.Instance.Pop("CatBall");
            catBall.transform.position = transform.position + (Vector3)spawnPos;
            catBall.transform.GetComponent<CatBall>().Movement(spawnPos, 5f);

            yield return new WaitForSeconds(_summonDelay); 
        }
    }
}
