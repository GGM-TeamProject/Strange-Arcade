using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trophy : MonoBehaviour
{
    private Image img;

    public TrophySO trophySO;

    private void Start() {
        img = GetComponent<Image>();
    }

    public void achiveMission(bool achive){
        img.sprite = (achive) ? trophySO.unLocked : trophySO.locked; 
    }
}
