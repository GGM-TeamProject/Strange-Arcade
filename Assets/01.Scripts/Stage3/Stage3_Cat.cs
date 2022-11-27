using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_Cat : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _moveMinValue = 0f;
    [SerializeField] private float _moveMaxValue = 0f;
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private Stage3_Car _player;

    [SerializeField] private AudioClip _itemSpawnSound;

    private bool _isSpawnItem = false;
    private Transform _model;
    private Animator _anim;
    private Vector3 _moveDir = Vector3.right;

    public void Init(){
        _model = transform.Find("Model");
        _anim = _model.GetComponent<Animator>();
        StopAllCoroutines();
        StartCoroutine(UpdatePath());
    }

    private void Update() {
        Move();
    }

    private void Move(){
        _anim.SetBool("IsMove", !_isSpawnItem);
        if(_isSpawnItem == false){
            transform.position += _moveDir * _moveSpeed * Time.deltaTime;
            if(transform.position.x <= _moveMinValue || transform.position.x >= _moveMaxValue){
                _moveDir *= -1f;
            }
        }
    }

    IEnumerator UpdatePath(){
        while(_player.PlayerState != CarState.Die){
            int randPercentage = Random.Range(1, 5);
            _spawnDelay = Mathf.Lerp(3, 1, _player.PlayerSpeed / 100);
            yield return new WaitForSeconds(_spawnDelay);

            _isSpawnItem = true;
            _anim.SetTrigger("IsThrow");
            Vector3 _spawnPos = new Vector3(transform.position.x, -11, 55);
            GameObject item = SetRandomItem(randPercentage);
            item.transform.position = _spawnPos;
            GameManager.Instance.SoundManager.PlayerOneShot(_itemSpawnSound);
            yield return new WaitForSeconds(0.5f);
            _isSpawnItem = false;
        }
    }

    private GameObject SetRandomItem(int percentage){
        GameObject item = null;
        if(percentage == 1){ //츄르
            item = PoolManager.Instance.Pop("Chur");
        }
        else if(percentage == 2){ //바나나
            item = PoolManager.Instance.Pop("Banana");
        }
        else{ //라바콘
            item = PoolManager.Instance.Pop("Labacon");
        }
        return item;
    }
}
