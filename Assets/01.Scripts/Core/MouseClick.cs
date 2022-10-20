using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;

    private void Update() {
        RaycastHit hit = CastRay();
        // if(hit.collider != null){
        //     GameManager.Instance.CursorManager.mouseState = CursorManager.MouseState.Select;
        // }
        // else{
        //     GameManager.Instance.CursorManager.mouseState = CursorManager.MouseState.Normal;
        // }
    }

    private RaycastHit CastRay(){
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out RaycastHit hit, Mathf.Infinity, targetLayer);

        return hit;
    }
}
