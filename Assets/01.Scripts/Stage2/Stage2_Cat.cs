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
    [SerializeField] private Vector3 _initPos = new Vector3(0, 0);

    [SerializeField] private float _maxHp;
    [SerializeField] private UnityEvent _callBack = null;
    private float _currentHp;

    [SerializeField] private AudioClip _catHitSound;
    [SerializeField] private AudioClip _catMoveSound;

    private CatState _catState = CatState.Idle;
    public CatState CatState => _catState;

    private SpriteRenderer _spriteRenderer;
    private Stage2_UI _ui;

    private void Awake() {
        _ui = transform.parent.Find("Stage2Canvas").GetComponent<Stage2_UI>();
        _spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void OnEnable() {
        _callBack?.Invoke();
    }

    public void Init(){
        _currentHp = _maxHp;
        _catState = CatState.GodMode;
        transform.position = _initPos;
        Invoke("CancleGodMode", 0.2f);
        StopAllCoroutines();
        StartCoroutine(Move());
    }

    private void CancleGodMode(){
        _catState = CatState.Idle;
    }

    IEnumerator Move(){
        while(_catState != CatState.Die){
            float spawnTime = Random.Range(5f, 10f);
            yield return new WaitForSeconds(spawnTime);
            Vector2 spawnPos = new Vector2(Random.Range(_stageData.minPos.x, _stageData.maxPos.x),
                Random.Range(_stageData.minPos.y, _stageData.maxPos.y));

            Sequence sq = DOTween.Sequence();
            sq.Append(_spriteRenderer.DOFade(0.3f, 0.1f));
            sq.OnComplete(() => {
                _spriteRenderer.DOFade(1f, 0.1f);
                GameManager.Instance.SoundManager.PlayerOneShot(_catMoveSound);
                transform.position = spawnPos;
            });
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void OnDamage(float damage, UnityEvent CallBack = null)
    {
        if(_catState == CatState.GodMode) return;
        
        GameManager.Instance.SoundManager.PlayerOneShot(_catHitSound);
        _ui.HpDownUI((int)damage);
        _currentHp -= damage;
        if(_currentHp <= 0 && _catState != CatState.Die && !SceneTransManager.Instance.IsChangeScene &&
            !GameManager.Instance.UIManager.IsGameClear && !GameManager.Instance.UIManager.IsGameOver){
            _catState = CatState.Die;
            OnPlayerDie(_callBack);
        }
    }

    private void OnPlayerDie(UnityEvent CallBack){
        Debug.Log("주금");
        GameManager.Instance.ChallengeManager.CheckClear("FirstDeath_S2");
        StartCoroutine(PlayerDieCoroutine(CallBack));
    }

    IEnumerator PlayerDieCoroutine(UnityEvent CallBack){
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameOverPanel(false);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R)); //나중에 수정
        _currentHp = _maxHp;
        CallBack?.Invoke();
        GameManager.Instance.UIManager.OffGameOverPanel(false);
    }
}
