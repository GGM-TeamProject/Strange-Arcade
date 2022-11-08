using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerEnum{
    Idle,
    JumpReady,
    Jump,
    Hit
}

public class Player_Stage1 : MonoBehaviour
{
    [Header("Movement Value")]
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPowerUpSpeed;

    [Header("JumpPowerLimitValue")]
    [SerializeField] private float _maxJumpPower;
    [SerializeField] private float _minJumpPower;

    [Header("PlayerEnumSet")]
    [SerializeField] private PlayerEnum _playerEnum = PlayerEnum.Idle;

    private Rigidbody2D _rigid;
    private Animator _anim;
    private Transform _sprite;
    private Player_Stage1_JumpGauge _jumpGauge;
    private CapsuleCollider2D _capsuleCollider2D;
    private ParticleSystem _playerRunParticle;

    private int _jumpCount = 1;
    private float _horizontalInput = 0;
    private bool _isJump = false;
    private bool _isMove = false;
    private bool _isActiveGauge = false;

    
    private void Awake() {
        _sprite = transform.Find("Sprite");
        _anim = _sprite.GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _jumpGauge = transform.Find("JumpGauge").GetComponent<Player_Stage1_JumpGauge>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _playerRunParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Start() {
        GaugePopUp(false, 0f);
        GameManager.Instance.CameraManager.CamSetting(transform, false, true, new Vector2(0, 2f));
    }

    private void Update() {
        Move();
        Jump();
        AnimationSet();
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

        if(!_isJump){
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
                _jumpCount--;
                _playerEnum = PlayerEnum.Jump;
                _isJump = true;

                //summon Jump Particle
                StartCoroutine(PlayJumpParticle(new Vector2(_capsuleCollider2D.bounds.center.x, _capsuleCollider2D.bounds.min.y)));

                _rigid.velocity = new Vector3(_horizontalInput * (_jumpPower * 0.5f), _jumpPower * 1.2f);
                _jumpPower = 0;
            }
        }
    }

    IEnumerator PlayJumpParticle(Vector2 summonPos){
        GameObject jumpParticle = PoolManager.Instance.Pop("PlayerJumpEffect");
        jumpParticle.transform.position = summonPos;
        yield return new WaitUntil(() => !jumpParticle.GetComponent<ParticleSystem>().isPlaying);
        PoolManager.Instance.Push(jumpParticle);
    }

    private void GaugePopUp(bool isActive, float delayTime){
        _isActiveGauge = isActive;
        SpriteRenderer valueSprite = _jumpGauge.ValueTrm.GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer backGround = _jumpGauge.ValueTrm.GetComponentInParent<SpriteRenderer>();

        backGround.DOFade((isActive ? 1 : 0), delayTime);
        valueSprite.DOFade((isActive ? 1 : 0), delayTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Platform")){
            _playerEnum = PlayerEnum.Idle;
            _isJump = false;
            _jumpCount = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Lava")){
            _playerEnum = PlayerEnum.Hit;
            IDamage damage = transform.GetComponent<IDamage>();
            damage.OnDamage(10);
        }
    }
}
