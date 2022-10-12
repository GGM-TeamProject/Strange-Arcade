using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] private List<Trophy> _trophies;
    public List<Trophy> Trophies {get => _trophies;}

    private Dictionary<string, bool> challengeCheck = new Dictionary<string, bool>(){
        {"FirstGame", false},

        {"Clear_S1", false},
        {"Clear_S2", false},
        {"Clear_S3", false},
        {"Clear_S4", false},

        {"FirstDeath_S1", false},
        {"FirstDeath_S2", false},
        {"FirstDeath_S3", false},
        {"FirstDeath_S4", false},

        {"AllClear", false}
    };

    private void Awake() {

    }
}
