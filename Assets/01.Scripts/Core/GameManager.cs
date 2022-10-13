using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public ChallengeManager ChallengeManager;
    public UIManager UIManager;

    private void Awake() {
        ChallengeManager = GetComponent<ChallengeManager>();
        UIManager = GetComponent<UIManager>();
    }
}
