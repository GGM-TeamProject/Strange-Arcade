using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawners = new List<Transform>();

    private void Start() {
        foreach(Transform t in transform){
            if(t == transform) return;
            _spawners.Add(t);
        }
    }
}
