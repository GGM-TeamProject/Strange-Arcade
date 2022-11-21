using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage2_PlayerSkill : MonoBehaviour
{
    public float SkillCool;
    public float SkillDuration;
    public bool CanSkill = true; 

    public Action CallBackAction = null;
    
    public abstract void OnSkill();

    protected IEnumerator CoolDown(float normalCool){
        CanSkill = false;
        yield return new WaitForSeconds(SkillDuration);
        CallBackAction?.Invoke();
        while(SkillCool >= 0){
            SkillCool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        CanSkill = true;
        SkillCool = normalCool;
    }
}
