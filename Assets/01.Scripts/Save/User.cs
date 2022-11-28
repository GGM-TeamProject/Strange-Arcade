using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int CurrentPlayStage = 1;
    public int _CurrentPlayStage {
        get => CurrentPlayStage;
        set{
            if(value <= 0 || value >= MaxPlayStage) return;
            CurrentPlayStage = value;
        }
    }
    public const int MaxPlayStage = 3;
    public bool[] clearChallenge = new bool[8];
}
