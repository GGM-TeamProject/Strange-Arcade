using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lava : MonoBehaviour
{
    [SerializeField] private float _lavaMoveSpeed = 0.5f;
    [SerializeField] private Vector3 _initPos;
    [SerializeField] private TextMeshProUGUI _warningTxt;

    private Player_Stage1 _player;

    private bool _onMove = true;
    public bool OnMove {get => _onMove; set => _onMove = value;}

    private void Update() {
        if(_onMove){
            _warningTxt.gameObject.SetActive((_player.transform.position - transform.position).magnitude <= 25f);
            transform.position += Vector3.up * Time.deltaTime * _lavaMoveSpeed;
        }
    }

    public void InitSet(){
        _onMove = true;
        _player = transform.parent.Find("Player").GetComponent<Player_Stage1>();
        transform.position = _initPos;
    }
}
