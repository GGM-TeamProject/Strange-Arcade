using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [System.Serializable]
    public enum MouseState{
        Normal,
        Select,
        Waiting
    }

    [field:SerializeField] public MouseState mouseState {get; set;}

    private Texture2D _normalCursor;
    private Texture2D _selectCursor;
    private Texture2D _waitingCursor;

    private void Start() {
        _normalCursor = Resources.Load<Texture2D>("Cursors/NormalCursor");
        _selectCursor = Resources.Load<Texture2D>("Cursors/SelectCursor");
        _waitingCursor = Resources.Load<Texture2D>("Cursors/WaitingCursor");
    }

    private void Update() {
        SetMouseCursor();
    }

    private void SetMouseCursor(){
        Texture2D texture = null;

        switch(mouseState){
            case MouseState.Normal:
                texture = _normalCursor;
                break;
            case MouseState.Select:
                texture = _selectCursor;
                break;
            case MouseState.Waiting:
                texture = _waitingCursor;
                break;
        }

        Cursor.SetCursor(texture, new Vector2(0, 0), CursorMode.Auto);
    }

    public void SetCursor(bool _bool){
        Cursor.visible = _bool;
        Cursor.lockState = (_bool) ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
