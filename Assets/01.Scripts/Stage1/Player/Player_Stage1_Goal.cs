using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stage1_Goal : MonoBehaviour
{
    [System.Serializable]
    struct Height{
        public float max;
        public float min;
    }

    [Header("Climbing Value")]
    [SerializeField] private Height h;

    private Slider _playerTransformSlider;

    private void Awake() {
        _playerTransformSlider = transform.parent.Find("Canvas/Slider").GetComponent<Slider>();
    }

    private void Start() {
        _playerTransformSlider.maxValue = h.max;
        _playerTransformSlider.minValue = h.min;
    }

    private void Update() {
        _playerTransformSlider.value = Mathf.Lerp(h.min, h.max, transform.position.y / 100);
    }
}
