using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<string, Stack<GameObject>> _pools = new Dictionary<string,  Stack<GameObject>>();

    private GameObject _prefab;
    private Transform _trmParent;

    public void CreatePool(GameObject prefab, Transform parent, int cnt = 10)
    {
        _prefab = prefab;
        _trmParent = parent;
        _pools.Add(_prefab.name, new Stack<GameObject>());
        for (int i = 0; i < cnt; i++)
        {
            GameObject obj;
            obj = GameObject.Instantiate (_prefab, _trmParent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.SetActive(false);
            _pools[obj.name].Push(obj);
        }
    }

    public GameObject Pop(string prefabName)
    {
        if(_pools.ContainsKey(prefabName) == false)
        {
            Debug.LogError("Obj hasent exist in PoolList");
        }

        GameObject obj = null;
        if(_pools[prefabName].Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _trmParent);
            obj.name = obj.name.Replace("(Clone)", "");
        }
        else
        {
            obj = _pools[prefabName].Pop();
            obj.SetActive(true);
        }
        return obj;
    }

    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        _pools[obj.name].Push(obj);
    }
}
