using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stage2 : MonoBehaviour
{
    [Header("Movement Value")]
    [SerializeField] private float _accel = 50f;
    [SerializeField] private float _deAccel = 50f;
    [SerializeField][Range(0.1f, 10f)] private float _maxSpeed = 10f;

    private Rigidbody2D _rigid;
    private Animator _anim;
    private Stage2_Cat _cat;

    [SerializeField] private Stage2_PlayerSkill[] _playerSkills;
    public Stage2_PlayerSkill[] PlayerSkills => _playerSkills;

    private float _currentVelocity = 0f;
    private Vector2 _movementDirection;

    private void Awake() {
        _anim = transform.Find("Sprite").GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _cat = transform.parent.Find("Cat").GetComponent<Stage2_Cat>();
    }

    private void Start() {
        SkillReset();
    }

    private void Update() {
        UseSkill();
    }

    public void SkillReset(){
        foreach(Stage2_PlayerSkill skill in _playerSkills){
            if(!skill.CanSkill){
                skill.SkillCool = 0f;
                skill.CallBackAction?.Invoke();
            }
        }
    }

    #region MoveAgent
    public void MoveAgent(Vector2 movementInput){
        if(movementInput.sqrMagnitude > 0){
            if(Vector2.Dot(movementInput, _movementDirection) < 0){
                _currentVelocity = 0f;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalcSpeed(movementInput);
    }

    private float CalcSpeed(Vector2 movementInput){
        if(movementInput.sqrMagnitude > 0){
            _currentVelocity += _accel * Time.deltaTime;
        }
        else{
            _currentVelocity -= _deAccel * Time.deltaTime;
        }
        return Mathf.Clamp(_currentVelocity, 0, _maxSpeed);
    }

    private void FixedUpdate() {
        _rigid.velocity = _movementDirection * _currentVelocity;
        _anim.SetBool("IsMove", _rigid.velocity.magnitude > 0);
    }

    public void StopImmediatly(){
        _currentVelocity = 0f;
        _rigid.velocity = Vector2.zero;
    }
    #endregion

    private void UseSkill(){
        if(Input.GetKeyDown(KeyCode.Z)){
            _playerSkills[0].OnSkill();
        }
        else if(Input.GetKeyDown(KeyCode.X)){
            _playerSkills[1].OnSkill();
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            _playerSkills[2].OnSkill();
        }
    }
}
