using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum PlayerEnum{
    Idle,
    JumpReady,
    Jump,
    Hit,
    Die
}

public class Player_Stage1 : MonoBehaviour
{
    [Header("PlayerDieAction")]
    [SerializeField] private UnityEvent _playerDieAction;

    [Header("InitPos")]
    [SerializeField] private Vector3 _initPos;

    [Header("Movement Value")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPowerUpSpeed;

    [Header("JumpPowerLimitValue")]
    [SerializeField] private float _maxJumpPower;
    [SerializeField] private float _minJumpPower;

    [Header("PlayerDamageValue")]
    [SerializeField] private float _normalDamage = 1f;
    [SerializeField] private float _instantDeathDamage = 100f;
    [SerializeField] private float _stunTime = 3f;
 
    [Header("PlayerEnumSet")]
    [SerializeField] private PlayerEnum _playerEnum = PlayerEnum.Idle;
    public PlayerEnum PlayerEnum {get => _playerEnum; set => _playerEnum = value;}

    private Rigidbody2D _rigid;
    private Animator _anim;
    private Transform _sprite;
    private Player_Stage1_JumpGauge _jumpGauge;
    private CapsuleCollider2D _capsuleCollider2D;
    private ParticleSystem _playerRunParticle;
    private Animator _playerHitAnimation;
    private ParticleSystem _playerStunParticle;

    private float _horizontalInput = 0;
    private bool _isJump = false;
    private bool _onPlatform = false;
    private bool _isMove = false;
    private bool _isActiveGauge = false;

    
    private void Awake() {
        _sprite = transform.Find("Sprite");
        _anim = _sprite.GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _jumpGauge = transform.Find("JumpGauge").GetComponent<Player_Stage1_JumpGauge>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _playerRunParticle = transform.Find("Sprite/PlayerRunEffect").GetComponent<ParticleSystem>();
        _playerStunParticle = transform.Find("StunParticle").GetComponent<ParticleSystem>();
        _playerHitAnimation = transform.Find("PlayerHitAnimation").GetComponent<Animator>();
    }

    private void Update() {
        if(_playerEnum == PlayerEnum.Die) return;

        PlatformCheck();
        AnimationSet();

        if(!(_playerEnum == PlayerEnum.Hit)){
            Move();
            Jump();
        }
    }

    private void OnEnable() {
        GameManager.Instance.CameraManager.CamSetting(transform, false, true, new Vector2(0, 2f));
        _playerDieAction?.Invoke();
    }

    public void InitSetting(){
        transform.position = _initPos;
    }

    private void AnimationSet(){
        _anim.SetBool("IsMove", (_isMove && _playerEnum == PlayerEnum.Idle));
        _anim.SetBool("IsJump", _isJump);
    }

    private void Move(){
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _isMove = _horizontalInput != 0;

        if(_playerEnum != PlayerEnum.Jump){
            FlipSprite(_horizontalInput);

            if(_playerEnum == PlayerEnum.Idle){
                //Player Run Particle Play
                if(_horizontalInput != 0 && !_playerRunParticle.isPlaying) _playerRunParticle.Play();
                else if(_horizontalInput == 0 && _playerRunParticle.isPlaying) _playerRunParticle.Stop();

                Vector3 moveDir = new Vector3(_horizontalInput * _speed, _rigid.velocity.y);
                _rigid.velocity = moveDir;
            }
        }
    }

    private void FlipSprite(float x){
        if(x > 0){
            _sprite.localScale = new Vector3(1, 1, 1);
        }
        else if(x < 0){
            _sprite.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Jump(){
        _jumpGauge.SetJumpGauge(_jumpPower);

        if(!_isJump && _onPlatform){
            if(Input.GetKey(KeyCode.Space)){
                if(_playerRunParticle.isPlaying) _playerRunParticle.Stop();
                if(!_isActiveGauge) GaugePopUp(true, 1f);
                _rigid.velocity = Vector2.zero;
                _playerEnum = PlayerEnum.JumpReady;
                _jumpPower += Time.deltaTime * _jumpPowerUpSpeed;
                _jumpPower = Mathf.Clamp(_jumpPower, _minJumpPower, _maxJumpPower);
            }
            else{
                if(_isActiveGauge) GaugePopUp(false, 1f);
            }

            if(Input.GetKeyUp(KeyCode.Space)){
                _playerEnum = PlayerEnum.Jump;
                _isJump = true;

                //summon Jump Particle
                StartCoroutine(PlayJumpParticle(new Vector2(_capsuleCollider2D.bounds.center.x, _capsuleCollider2D.bounds.min.y)));

                _rigid.velocity = new Vector3(_horizontalInput * (_jumpPower * 0.5f), _jumpPower * 1.2f);
                _jumpPower = 0;
            }
        }
    }

    private void GaugePopUp(bool isActive, float delayTime){
        _isActiveGauge = isActive;
        SpriteRenderer valueSprite = _jumpGauge.ValueTrm.GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer backGround = _jumpGauge.ValueTrm.GetComponentInParent<SpriteRenderer>();

        backGround.DOFade((isActive ? 1 : 0), delayTime);
        valueSprite.DOFade((isActive ? 1 : 0), delayTime);
    }

    private void PlatformCheck(){
        RaycastHit2D hit = Physics2D.CapsuleCast(_capsuleCollider2D.bounds.center, _capsuleCollider2D.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, LayerMask.GetMask("Platform"));

        _onPlatform = hit;
    }

    IEnumerator PlayJumpParticle(Vector2 summonPos){
        GameObject jumpParticle = PoolManager.Instance.Pop("PlayerJumpEffect");
        jumpParticle.transform.position = summonPos;
        yield return new WaitUntil(() => !jumpParticle.GetComponent<ParticleSystem>().isPlaying);
        PoolManager.Instance.Push(jumpParticle);
    }

    IEnumerator PlayerStun(float stunTime){
        _playerHitAnimation.Play("PlayerHit");
        _playerStunParticle.Play();
        yield return new WaitForSeconds(stunTime);
        _playerStunParticle.Stop();
        _playerEnum = PlayerEnum.Idle;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 8){
            if(_playerEnum != PlayerEnum.Hit) _playerEnum = PlayerEnum.Idle;
            _isJump = false;
        }

        if(other.transform.CompareTag("Enemy")){
            _playerEnum = PlayerEnum.Hit;
            _rigid.velocity = Vector2.zero;

            StopCoroutine(PlayerStun(_stunTime));
            StartCoroutine(PlayerStun(_stunTime));

            IDamage damage = transform.GetComponent<IDamage>();
            damage.OnDamage(_normalDamage, _playerDieAction);

            PoolManager.Instance.Push(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Lava") && _playerEnum != PlayerEnum.Die){
            _playerEnum = PlayerEnum.Die;

            _rigid.velocity = Vector2.zero;
            _rigid.velocity = Vector2.up * 6;

            other.transform.GetComponent<Lava>().OnMove = false;
            IDamage damage = transform.GetComponent<IDamage>();
            damage.OnDamage(_instantDeathDamage, _playerDieAction);
        }

        if(other.transform.CompareTag("Cat") && _playerEnum != PlayerEnum.Die){
            GameManager.Instance.ChallengeManager.CheckClear("Clear_S1");
            StartCoroutine(StageClearCoroutine());
        }
    }

    IEnumerator StageClearCoroutine(){
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameClearPanel(false);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape)); //나중에 수정
        GameManager.Instance.UIManager.OffGameClearPanel(false);
        SceneTransManager.Instance.SceneChange("MainMenu");
    }
}
