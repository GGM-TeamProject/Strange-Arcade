using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SceneTransManager : MonoSingleton<SceneTransManager>
{
    [SerializeField] private Camera[] _cameras;

    [SerializeField] private UniversalRendererData _pixelizeData;
    [SerializeField] private PixelizeFeature.CustomPassSetting _passSetting;

    [SerializeField] private AudioClip _mainMenuBGM;
    [SerializeField] private AudioClip _stage1BGM;
    [SerializeField] private AudioClip _stage2BGM;
    [SerializeField] private AudioClip _stage3BGM;

    private bool _isChangeScene = false;

    private PixelizeFeature _pixelizeFeature;
    private Transform _screen;
    private Dictionary<string, Transform> _stages = new Dictionary<string, Transform>();

    private void Awake() {
        _screen = GameObject.Find("Screen").transform;
        _stages.Add("MainMenu", _screen.Find("Menu"));
        _stages.Add("Stage1", _screen.Find("Stages/Stage_1"));
        _stages.Add("Stage2", _screen.Find("Stages/Stage_2"));
        _stages.Add("Stage3", _screen.Find("Stages/Stage_3"));

        _pixelizeFeature = (PixelizeFeature)_pixelizeData.rendererFeatures[0];
    }

    private void Update() {
        _pixelizeFeature.settings = _passSetting;
    }

    public void SceneChange(string sceneName){
        if(!_isChangeScene){
            StartCoroutine(SceneChangeCoroutine(sceneName));
        }
    }

    IEnumerator SceneChangeCoroutine(string sceneName){
        _isChangeScene = true;
        SetCameraRenderer(1);
        GameManager.Instance.CursorManager.mouseState = CursorManager.MouseState.Waiting;

        while(_passSetting.screenHeight >= 20){
            _passSetting.screenHeight -= 1;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(1f);

        foreach(var stage in _stages){
            if(stage.Key == sceneName) stage.Value.gameObject.SetActive(true);
            else stage.Value.gameObject.SetActive(false);
        }

        while(_passSetting.screenHeight <= 100){
            _passSetting.screenHeight += 1;
            yield return new WaitForFixedUpdate();
        }

        SetCameraRenderer(0);
        switch(sceneName){
            case "MainMenu":
                GameManager.Instance.SoundManager.BGMSetting(_mainMenuBGM);
                break;
            case "Stage1":
                GameManager.Instance.SoundManager.BGMSetting(_stage1BGM);
                break;
            case "Stage2":
                GameManager.Instance.SoundManager.BGMSetting(_stage2BGM);
                break;
            case "Stage3":
                GameManager.Instance.SoundManager.BGMSetting(_stage3BGM);
                break;
        }
        GameManager.Instance.CursorManager.mouseState = CursorManager.MouseState.Normal;
        _isChangeScene = false;
    }

    private void SetCameraRenderer(int renderer){
        foreach(Camera cam in _cameras){
            cam.GetUniversalAdditionalCameraData().SetRenderer(renderer);
        }
    }
}
