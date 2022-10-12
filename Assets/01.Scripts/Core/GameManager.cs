using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake() {
        ChallengeManager challengeManager = GetComponent<ChallengeManager>();
    }
}
