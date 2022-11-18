using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_AllKillBullet : Stage2_PlayerSkill
{   
    public override void OnSkill()
    {
        if(CanSkill){
            StartCoroutine(CoolDown(SkillCool));
            foreach(Stage2_Bullet bullet in transform.parent.Find("RollObjects/Bullets").GetComponentsInChildren<Stage2_Bullet>()){
                bullet.OnKill();
            }
        }
    }
}
