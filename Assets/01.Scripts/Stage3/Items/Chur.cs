using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chur : MonoBehaviour, Iitem
{
    private List<SpriteRenderer> _churBombs = new List<SpriteRenderer>();

    private void Awake() {
        foreach(Transform churBomb in GameObject.Find("Screen/Stages/Stage_3/ChurBomb").transform){
            _churBombs.Add(churBomb.GetComponent<SpriteRenderer>());
        }
    }

    [ContextMenu("asdf")]
    public void OnUseItem()
    {
        StartCoroutine(ChurAttack());
    }

    IEnumerator ChurAttack(){
        for(int i = 0; i < _churBombs.Count; i++){
            _churBombs[i].transform.DOScale((i == 0) ? 2.7f : 2f, 1f);
            _churBombs[i].DOFade(1f, 1f);

            yield return new WaitForSeconds(0.3f);
        }

        for(int i = 0; i < _churBombs.Count; i++){
            _churBombs[i].DOFade(0f, 4f);
        }
        
        yield return new WaitForSeconds(4f);

        for(int i = 0; i < _churBombs.Count; i++){
            _churBombs[i].transform.DOScale(0f, 0.1f);
        }
    }
}
