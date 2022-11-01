using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerEnum{
    Idle,
    JumpReady,
    Jump
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

    private int _jumpCount = 1;
    private float _horizontalInput = 0;
    private bool _isJump = false;
    private bool _isMove = false;

    
    private void Awake() {
        _sprite = transform.Find("Sprite");
        _anim = _sprite.GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _jumpGauge = transform.Find("JumpGauge").GetComponent<Player_Stage1_JumpGauge>();
    }

    private void Update() {
        Move();
        Jump();
        AnimationSet();
    }

    private void AnimationSet(){
        _anim.SetBool("IsMove", _isMove);
        _anim.SetBool("IsJump", _isJump);
    }

    private void Move(){
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _isMove = _horizontalInput != 0;

        if(_playerEnum == PlayerEnum.Idle){
            Vector3 moveDir = new Vector3(_horizontalInput * _speed, _rigid.velocity.y);
            _rigid.velocity = moveDir;
        }
    }

    private void Jump(){
        _jumpGauge.SetJumpGauge(_jumpPower);

        if(!_isJump){
            if(Input.GetKey(KeyCode.Space)){
                _rigid.velocity = Vector2.zero;
                _playerEnum = PlayerEnum.JumpReady;
                _jumpPower += Time.deltaTime * _jumpPowerUpSpeed;
                _jumpPower = Mathf.Clamp(_jumpPower, _minJumpPower, _maxJumpPower);
            }

            if(Input.GetKeyUp(KeyCode.Space)){
                _jumpCount--;
                _playerEnum = PlayerEnum.Jump;
                _isJump = true;

                _rigid.velocity = new Vector3(_horizontalInput * (_jumpPower * 0.5f), _jumpPower * 1.2f);
                _jumpPower = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Platform")){
            _playerEnum = PlayerEnum.Idle;
            _isJump = false;
            _jumpCount = 1;
        }
    }
}
