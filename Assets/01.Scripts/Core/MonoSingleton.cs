using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance{
        get{
            try{
                instance = GameObject.Find("Manager").GetComponentInChildren<T>();
            }
            catch(NullReferenceException){
                instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                instance.transform.SetParent(GameObject.Find("Manager").transform);
            }

            return instance;
        }
    }
}
