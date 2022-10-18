using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public void SetCursor(bool _bool){
        Cursor.visible = _bool;
        Cursor.lockState = (_bool) ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
