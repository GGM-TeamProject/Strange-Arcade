using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public enum CatState{
    Idle,
    GodMode,
    Die
}

public class Stage2_Cat : MonoBehaviour, IDamage
{
    [System.Serializable]
    struct StageData{
        public Vector2 maxPos;
        public Vector2 minPos;
    }
    [field:SerializeField] private StageData _stageData;

    [SerializeField] private float _maxHp;
    [SerializeField] private UnityEvent _callBack = null;
    private float _currentHp;

    private CatState _catState = CatState.Idle;
    public CatState CatState => _catState;

    private SpriteRenderer _spriteRenderer;
    private Stage2_UI _ui;

    private void Awake() {
        _ui = transform.parent.Find("Stage2Canvas").GetComponent<Stage2_UI>();
        _spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void Start() {
        Init();
    }

    public void Init(){
        _currentHp = _maxHp;
        _catState = CatState.GodMode;
        Invoke("CancleGodMode", 0.2f);
        StopAllCoroutines();
        StartCoroutine(Move());
    }

    private void CancleGodMode(){
        _catState = CatState.Idle;
    }

    IEnumerator Move(){
        while(_catState != CatState.Die){
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

    public void OnDamage(float damage, UnityEvent CallBack = null)
    {
        if(_catState == CatState.GodMode) return;
        
        _ui.HpDownUI((int)damage);
        _currentHp -= damage;
        if(_currentHp <= 0){
            OnPlayerDie(_callBack);
        }
    }

    private void OnPlayerDie(UnityEvent CallBack){
        Debug.Log("주금");
        _catState = CatState.Die;
        StartCoroutine(PlayerDieCoroutine(CallBack));
    }

    IEnumerator PlayerDieCoroutine(UnityEvent CallBack){
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameOverPanel();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R)); //나중에 수정
        _currentHp = _maxHp;
        CallBack?.Invoke();
        GameManager.Instance.UIManager.OffGameOverPanel();
    }
}
