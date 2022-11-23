using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePush : MonoBehaviour
{
    private void OnDisable() {
        PoolManager.Instance.Push(gameObject);
    }
}
