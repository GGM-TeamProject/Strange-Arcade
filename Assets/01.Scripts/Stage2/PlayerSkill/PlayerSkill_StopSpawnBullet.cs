using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_StopSpawnBullet : Stage2_PlayerSkill
{
    private BulletSpawner _bulletSpawner;

    private void Start() {
        _bulletSpawner = transform.parent.Find("RollObjects/BulletSpawners").GetComponent<BulletSpawner>();
        CallBackAction = () => OnSpawner();
    }

    public override void OnSkill()
    {
        OffSpawner();
        CoolDown(SkillCool);
    }

    private void OffSpawner() => _bulletSpawner.OnSpawnBulletAble(false);
    private void OnSpawner() => _bulletSpawner.OnSpawnBulletAble(true);
}
