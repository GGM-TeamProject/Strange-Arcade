using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemManager : MonoBehaviour
{
    private bool _attackRoutineIsRunning = false;
    public bool AttackRoutineIsRunning => _attackRoutineIsRunning;

    public void BananaMethod(float delay, ParticleSystem stunParticle, Stage3_CarInput inputSystem){
        StartCoroutine(BananaCallBack(delay, stunParticle, inputSystem));
    }

    public void ChurMehod(List<SpriteRenderer> churBombs){
        StartCoroutine(ChurAttack(churBombs));
    }

    public IEnumerator BananaCallBack(float delay, ParticleSystem stunParticle, Stage3_CarInput inputSystem){
        inputSystem._isMirror = true;
        yield return new WaitForSeconds(delay);
        stunParticle.Stop();
        inputSystem._isMirror = false;
    }

    public IEnumerator ChurAttack(List<SpriteRenderer> churBombs){
        _attackRoutineIsRunning = true;
        for(int i = 0; i < churBombs.Count; i++){
            churBombs[i].transform.DOScale((i == 0) ? 2.7f : 2f, 0.5f);
            churBombs[i].DOFade(1f, 0.5f);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);

        for(int i = 0; i < churBombs.Count; i++){
            churBombs[i].DOFade(0f, 4f);
        }
        
        yield return new WaitForSeconds(4f);

        for(int i = 0; i < churBombs.Count; i++){
            churBombs[i].transform.DOScale(0f, 0.1f);
        }
        _attackRoutineIsRunning = false;
    }
}
