using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    [System.Serializable]
    public enum MouseState{
        Normal,
        Select,
        Waiting
    }

    [SerializeField] private Camera _cam;
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
        MouseRay();
        SetMouseCursor();
    }

    private void MouseRay(){
        RaycastHit hit = CastRay();
        mouseState = (hit.collider) ? MouseState.Select : MouseState.Normal;
    }

    private RaycastHit CastRay(){
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.nearClipPlane);
        Vector3 worldMousePosFar = _cam.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = _cam.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, LayerMask.GetMask("MouseTarget"));
        Debug.DrawLine(worldMousePosNear, worldMousePosFar - worldMousePosNear);

        return hit;
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

        Cursor.SetCursor(texture, new Vector2(texture.width / 4, 0), CursorMode.Auto);
    }

    public void SetCursor(bool _bool){
        Cursor.visible = _bool;
        Cursor.lockState = (_bool) ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
