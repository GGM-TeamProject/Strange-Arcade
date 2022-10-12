using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TrophySO", menuName = "SO/TrophySO")]
public class TrophySO : ScriptableObject
{
    public int index;

    public Sprite unLocked;
    public Sprite locked;

    public string challegeName;
    public string explanation;
}
