using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage2_Cat : MonoBehaviour
{
    [System.Serializable]
    struct StageData{
        public Vector2 maxPos;
        public Vector2 minPos;
    }
    [field:SerializeField] private StageData _stageData;

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void Start() {
        StartCoroutine(Move());
    }

    IEnumerator Move(){
        while(true){
            float spawnTime = Random.Range(3f, 5f);
            yield return new WaitForSeconds(spawnTime);
            Vector2 spawnPos = new Vector2(Random.Range(_stageData.minPos.x, _stageData.maxPos.x),
                Random.Range(_stageData.minPos.y, _stageData.maxPos.y));

            Sequence sq = DOTween.Sequence();
            sq.Append(_spriteRenderer.DOFade(0.3f, 0.1f));
            sq.OnComplete(() => {
                _spriteRenderer.DOFade(1f, 0.1f);
                transform.position = spawnPos;
            });
            yield return new WaitForSeconds(0.2f);
        }
    }
}
