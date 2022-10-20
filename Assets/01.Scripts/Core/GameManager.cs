using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public ChallengeManager ChallengeManager;
    public UIManager UIManager;
    public CursorManager CursorManager;

    private void Awake() {
        ChallengeManager = GetComponent<ChallengeManager>();
        UIManager = GetComponent<UIManager>();
        CursorManager = GetComponent<CursorManager>();
    }
}
