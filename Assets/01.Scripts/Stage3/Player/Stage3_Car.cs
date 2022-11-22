using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using DG.Tweening;

public class Stage3_Car : MonoBehaviour, IDamage
{
    [Header("Car Move Value")]
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _minAccelPower = 25f;
    [SerializeField] private float _accelPower = 10f;
    [SerializeField] private float _deAccelPower = 7f;
    [SerializeField] private VisualEffect _speedLine;

    [Header("Gradient Preset")]
    [SerializeField] private Gradient _normalGradient;
    [SerializeField] private Gradient _neonGradient;

    [Header("Dash Value")]
    [SerializeField] private float _dashSpeed = 300f;
    [SerializeField] private float _dashDuration = 5f;
    [SerializeField] private float _dashCool = 5f;
    
    [SerializeField]private float _dashGauge = 0f;
    private bool _isDash = false;
    private float _currentTime = 0f;

    public float PlayerSpeed => _speed;
    public bool IsDash => _isDash;

    private void Update() {
        LineEffect();
        if(!_isDash){
            Accel();
            if(_dashGauge <= 100f) DashCoolTime();
        }
    }

    private void DashCoolTime(){
        _currentTime += Time.deltaTime;
        _dashGauge = Mathf.Lerp(0f, 100f, _currentTime / _dashCool);
        if(_currentTime >= _dashCool){
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                _isDash = true;
                _currentTime = 0f;
                StartCoroutine(DashCoroutine());
            }
        }
    }

    private IEnumerator DashCoroutine(){
        _speed = _dashSpeed;
        _speedLine.SetGradient("ColorGradient", _neonGradient);
        _speedLine.SetFloat("SpawnRate", 256);
        _speedLine.Play();
        transform.DOMoveZ(20f, 0.5f);
        //게이지 UI 줄이기
        yield return new WaitForSeconds(_dashDuration);
        _speedLine.SetFloat("SpawnRate", 128);
        _speedLine.SetGradient("ColorGradient", _normalGradient);
        transform.DOMoveZ(17f, 0.5f);
        _isDash = false;
    }

    private void Accel(){
        if(Input.GetKey(KeyCode.Space)){
            _speed += Time.deltaTime * _accelPower;
        }
        else{
            _speed -= Time.deltaTime * _deAccelPower;
        }

        _speed = Mathf.Clamp(_speed, 0f, 100f);
    }

    private void LineEffect(){
        if(_speed < _minAccelPower) _speedLine.Stop();
        else if(_speed <= 100) _speedLine.Play();
    }

    public void OnDamage(float damage, UnityEvent CallBack = null)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Item")){
            Iitem item = other.transform.GetComponent<Iitem>();
            item?.OnUseItem();
        }
    }
}
