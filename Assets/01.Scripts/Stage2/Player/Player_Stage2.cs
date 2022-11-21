using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stage2 : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private Animator _anim;
    private Stage2_Cat _cat;

    [SerializeField] private Stage2_PlayerSkill[] _playerSkills;
    public Stage2_PlayerSkill[] PlayerSkills => _playerSkills;

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

    private void FixedUpdate(){
        _anim.SetBool("IsMove", _rigid.velocity.magnitude > 0);
    }

    public void SkillReset(){
        foreach(Stage2_PlayerSkill skill in _playerSkills){
            if(!skill.CanSkill){
                skill.SkillCool = 0f;
                skill.CallBackAction?.Invoke();
            }
        }
    }

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
