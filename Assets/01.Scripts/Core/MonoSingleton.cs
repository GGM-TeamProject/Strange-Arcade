using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance{
        get{
            instance = GameObject.Find("Manager").GetComponentInChildren<T>();

            if(instance == null){
                Debug.Log(1);
                instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                instance.transform.SetParent(GameObject.Find("Manager").transform);
            }

            return instance;
        }
    }
}
