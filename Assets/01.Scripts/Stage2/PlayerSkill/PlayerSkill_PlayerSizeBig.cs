using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSkill_PlayerSizeBig : Stage2_PlayerSkill
{
    private void Start() {
        CallBackAction = () => ScaleDown();
    }

    public override void OnSkill()
    {
        ScaleUp();
        CoolDown(SkillCool);
    }

    private void ScaleUp() => transform.DOScale(3, 0.5f);
    private void ScaleDown() => transform.DOScale(1, 0.5f);
}
