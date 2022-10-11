using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophySetting : MonoBehaviour
{
    [SerializeField] private List<Image> _notFindImg;
    [SerializeField] private List<Image> _findImg;

    private Image[] _trophyImg;

    //JSON으로 도전과제 달성 확인하기

    private void Awake() {
        _trophyImg = GetComponentsInChildren<Image>();
    }
}
