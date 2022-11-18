using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage2_PlayerSkill : MonoBehaviour
{
    public float SkillCool;
    public float SkillDuration;
    public bool CanSkill = true; 

    protected Action CallBackAction = null;

    public abstract void OnSkill();

    protected IEnumerator CoolDown(float SkillCool){
        CanSkill = false;
        yield return new WaitForSeconds(SkillDuration);
        CallBackAction?.Invoke();
        while(this.SkillCool <= 0){
            this.SkillCool -= Time.deltaTime;
        }
        CanSkill = true;
        this.SkillCool = SkillCool;
    }
}
