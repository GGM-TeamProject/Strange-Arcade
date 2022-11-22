using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

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
    
    private float _dashGauge = 0f;
    private bool _isDash = false;
    private float _currentTime = 0f;

    private MeshTrail _meshTrail;
    private Image _dashValue;
    private TextMeshProUGUI _speedText;

    public float PlayerSpeed => _speed;
    public bool IsDash => _isDash;

    private void Awake() {
        _meshTrail = GetComponent<MeshTrail>();
        _dashValue = transform.parent.Find("Stage3Canvas/DashGauge/BackGround/ValueImage").GetComponent<Image>();
        _speedText = transform.parent.Find("Stage3Canvas/SpeedText").GetComponent<TextMeshProUGUI>();
    }

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
        _dashValue.fillAmount = Mathf.Lerp(0f, 1f, _currentTime / _dashCool);
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

        _meshTrail.ActiveTrail(_dashDuration);

        _speedLine.SetGradient("ColorGradient", _neonGradient);
        _speedLine.SetFloat("SpawnRate", 256);
        _speedLine.Play();

        transform.DOMoveZ(20f, 0.5f);

        _speedText.text = $"{(int)_speed}\n        KM/H";
        _speedText.DOColor(Color.red, 0.5f);
        _speedText.rectTransform.DOShakeRotation(_dashDuration, 10, 10, 45);
        _dashValue.DOFillAmount(0f, 0.5f);

        yield return new WaitForSeconds(_dashDuration);

        _speedLine.SetFloat("SpawnRate", 128);
        _speedLine.SetGradient("ColorGradient", _normalGradient);
        transform.DOMoveZ(17f, 0.5f);

        _speedText.DOColor(Color.white, 0.5f);
        _speedText.rectTransform.rotation = Quaternion.Euler(0, 0, -5);

        _isDash = false;
    }

    private void Accel(){
        if(Input.GetKey(KeyCode.Space)){
            _speed += Time.deltaTime * _accelPower;
        }
        else{
            _speed -= Time.deltaTime * _deAccelPower;
        }

        _speedText.text = $"{(int)_speed}\n        KM/H";
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
