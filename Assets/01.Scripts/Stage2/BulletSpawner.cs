using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawners = new List<Transform>();

    private string[] _attackActions = new string[3];
    private int _attackCnt = 0;

    private float _spawnDelay = 5f;
    private const float _spawnMinusTime = 30f;
    private float _currentTime = 0f;
    private RollObject _rollObj;

    private void Awake() {
        _rollObj = transform.parent.GetComponent<RollObject>();
    }

    private void Start() {
        foreach(Transform t in transform){
            if(t == transform) return;
            _spawners.Add(t);
        }

        _attackActions[0] = "SingleAttack";
        _attackActions[1] = "SectorAttack";
        _attackActions[2] = "TripleAttack";

        StartCoroutine(SpawnBullet());
    }   

    private void Update() {
        _currentTime += Time.deltaTime;

        if(_currentTime >= _spawnMinusTime){
            _spawnDelay--;
            _spawnDelay = Mathf.Clamp(_spawnDelay, 1, 5);
            _currentTime = 0;
        }
    }

    IEnumerator SpawnBullet(){
        while(true){
            if(!_rollObj.IsRoll){
                int currentAttack = Random.Range(0, _attackActions.Length);
                StartCoroutine(_attackActions[currentAttack], _spawners[_attackCnt]);
                _attackCnt++;
                if(_attackCnt >= 3) _attackCnt = 0;
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }

    IEnumerator SingleAttack(Transform spawnTrm = null){
        float shotAngle = Mathf.Round(spawnTrm.rotation.eulerAngles.z + 
            Random.Range(spawnTrm.rotation.z - 30, spawnTrm.rotation.z + 30) + 90);
        Vector3 shotPos = new Vector3(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
        
        GameObject bullet = PoolManager.Instance.Pop("Bullet");
        bullet.transform.position = spawnTrm.position + shotPos;
        bullet.transform.rotation = Quaternion.Euler(0, 0, shotAngle - 90);
        bullet.GetComponent<Stage2_Bullet>().SetDirection(shotPos.normalized);
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator SectorAttack(Transform spawnTrm){
        for(int angle = -30; angle <= 30; angle += 30){
            float shotAngle = Mathf.Round((spawnTrm.rotation.eulerAngles.z + angle) + 90);
            Vector3 shotPos = new Vector3(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));

            GameObject bullet = PoolManager.Instance.Pop("Bullet");
            bullet.transform.position = spawnTrm.position + shotPos;
            bullet.transform.rotation = Quaternion.Euler(0, 0, shotAngle - 90);
            bullet.GetComponent<Stage2_Bullet>().SetDirection(shotPos.normalized);
        }
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator TripleAttack(Transform spawnTrm){
        float shotAngle = Mathf.Round(spawnTrm.rotation.eulerAngles.z + 
            Random.Range(spawnTrm.rotation.z - 30, spawnTrm.rotation.z + 30) + 90);
        Vector3 shotPos = new Vector3(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));

        for(int i = 0; i < 3; i++){
            GameObject bullet = PoolManager.Instance.Pop("Bullet");
            bullet.transform.position = spawnTrm.position + shotPos;
            bullet.transform.rotation = Quaternion.Euler(0, 0, shotAngle - 90);
            bullet.GetComponent<Stage2_Bullet>().SetDirection(shotPos.normalized);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
