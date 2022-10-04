using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform[] _spawnPos;

    private void Start() {
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet(){
        while(true){ //나중에 수정 해야함
            for(int i = 0; i < _spawnPos.Length; i++){
                GameObject bullet = GameObject.Instantiate(_bullet, _spawnPos[i]);

            }
        }
    }
}
