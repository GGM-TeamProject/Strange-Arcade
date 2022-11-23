using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage3_CarInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMoveKeyPress;

    public bool _isMirror = false;

    private void Start() {
        Init();
    }

    private void Update() {
        GetMoveInput();
    }

    public void Init(){
        _isMirror = false;
    }

    private void GetMoveInput(){
        OnMoveKeyPress?.Invoke(new Vector2((_isMirror) ? -Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal"), 0));
    }
}
