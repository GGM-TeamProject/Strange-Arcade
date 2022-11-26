using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{
    [SerializeField] protected List<Transform> _buttons = new List<Transform>();
    [SerializeField] protected Transform _cursor;

    protected int _currentSelectButton = 0;

    protected void Start(){
        _cursor.position = _buttons[_currentSelectButton].position;
    }

    protected void ButtonMove(){
        if(Input.GetKeyDown(KeyCode.DownArrow))
            _currentSelectButton++;
        if(Input.GetKeyDown(KeyCode.UpArrow))
            _currentSelectButton--;

        _currentSelectButton = Mathf.Clamp(_currentSelectButton, 0, _buttons.Count - 1);

        _cursor.position = _buttons[_currentSelectButton].position;
    }
}
