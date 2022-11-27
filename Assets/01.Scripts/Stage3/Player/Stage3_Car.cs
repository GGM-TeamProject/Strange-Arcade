using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public enum CarState{
    Idle,
    GodMode,
    Hit,
    Die
}

public class Stage3_Car : MonoBehaviour, IDamage
{
    [Header("Car Move Value")]
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _minAccelPower = 25f;
    [SerializeField] private float _accelPower = 10f;
    [SerializeField] private VisualEffect _speedLine;

    [Header("Gradient Preset")]
    [SerializeField] private Gradient _normalGradient;
    [SerializeField] private Gradient _neonGradient;

    [Header("Material Set")]
    [SerializeField] private Material _normalMat;
    [SerializeField] private Material _hologramMat;

    [Header("Dash Value")]
    [SerializeField] private float _dashSpeed = 300f;
    [SerializeField] private float _dashDuration = 5f;
    [SerializeField] private float _dashCool = 5f;
    [SerializeField] private float _godModeDuration = 2f;

    [Header("Player HP Value")]
    [SerializeField] private float _maxHP = 5f;
    [SerializeField] private UnityEvent _callBack = null;

    private float _currentHP = 0f;
    private float _dashGauge = 0f;
    private bool _isDash = false;
    private float _currentTime = 0f;

    private CarState _playerState = CarState.Idle;

    private MeshTrail _meshTrail;
    private Image _dashValue;
    private TextMeshProUGUI _speedText;
    private MeshRenderer _carMainMesh;

    public float PlayerSpeed { get => _speed; set => _speed = value; }
    public CarState PlayerState => _playerState;
    public bool IsDash => _isDash;

    private void Awake() {
        _carMainMesh = transform.Find("BODY").GetComponent<MeshRenderer>();
        _meshTrail = GetComponent<MeshTrail>();
        _dashValue = transform.parent.Find("Stage3Canvas/DashGauge/BackGround/ValueImage").GetComponent<Image>();
        _speedText = transform.parent.Find("Stage3Canvas/SpeedText").GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        Init();
    }

    private void OnEnable() {
        _callBack?.Invoke();
    }

    public void Init(){
        _currentHP = _maxHP;
        _playerState = CarState.Idle;
        _dashGauge = 0;
        _currentTime = 0;
        _speed = 0;
    }

    private void Update() {
        _carMainMesh.material = (_playerState == CarState.GodMode) ? _hologramMat : _normalMat;

        LineEffect();
        if(!_isDash){
            Accel();
            if(_dashGauge <= 100f) DashCoolTime();
        }
    }

    IEnumerator GodMode(float duration){
        _playerState = CarState.GodMode;
        yield return new WaitForSeconds(duration);
        _playerState = CarState.Idle;
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
        StartCoroutine(GodMode(_dashDuration));
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
        if(_playerState == CarState.Idle){
            _speed += Time.deltaTime * _accelPower;
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
        if(_playerState == CarState.GodMode) return;
        _currentHP--;
        if(_currentHP <= 0){
            OnPlayerDie(_callBack);
        }
        else{
            StartCoroutine(GodMode(_godModeDuration));
        }
    }

    private void OnPlayerDie(UnityEvent CallBack){
        Debug.Log("주금");
        GameManager.Instance.ChallengeManager.CheckClear("FirstDeath_S3");
        _playerState = CarState.Die;
        StartCoroutine(PlayerDieCoroutine(CallBack));
    }

    IEnumerator PlayerDieCoroutine(UnityEvent CallBack){
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameOverPanel(true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R)); //나중에 수정
        _currentHP = _maxHP;
        CallBack?.Invoke();
        GameManager.Instance.UIManager.OffGameOverPanel(true);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Item")){
            Item item = other.transform.GetComponent<Item>();
            if(_playerState != CarState.GodMode){
                item?.OnUseItem();
                OnDamage(1f);
            }

            PoolManager.Instance.Push(other.gameObject);
            GameObject impactParticle = PoolManager.Instance.Pop("ImpactParticle");
            impactParticle.transform.position = other.transform.position;
        }
    }

    private void OnDisable() {
        _speedLine.Stop();
    }
}
