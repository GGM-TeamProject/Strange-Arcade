using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Stage2_MoveInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMoveKeyPress;

    private void Update(){
        GetMoveInput();
    }

    private void GetMoveInput(){
        OnMoveKeyPress?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
