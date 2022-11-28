using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _unlock;
    [SerializeField] private Transform _lock;

    [field:SerializeField] public bool IsLock {get; set;} = true;

    public void SetLockState(bool lockState){
        IsLock = lockState;
        if(lockState){
            _lock.gameObject.SetActive(true);
            _unlock.gameObject.SetActive(false);
        }
        else{
            _lock.gameObject.SetActive(false);
            _unlock.gameObject.SetActive(true);
        }
    }
}
