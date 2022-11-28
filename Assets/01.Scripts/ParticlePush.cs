using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePush : MonoBehaviour
{
    private PoolManager _poolInstance;

    private void Awake() {
        _poolInstance = PoolManager.Instance;
    }

    private void OnDisable() {
        _poolInstance.Push(gameObject);
    }
}
