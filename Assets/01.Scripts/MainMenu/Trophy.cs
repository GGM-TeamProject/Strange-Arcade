using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trophy : MonoBehaviour
{
    private Image img;

    public TrophySO trophySO;

    public void achiveMission(bool achive){
        img = GetComponent<Image>();
        img.sprite = (achive) ? trophySO.unLocked : trophySO.locked; 
    }
}
