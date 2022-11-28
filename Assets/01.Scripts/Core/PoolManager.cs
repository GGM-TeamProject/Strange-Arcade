using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<string, Stack<GameObject>> _pools = new Dictionary<string,  Stack<GameObject>>();
    private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, Transform> _trmParents = new Dictionary<string, Transform>();

    public void CreatePool(GameObject prefab, Transform parent, int cnt = 10)
    {
        _prefabs.Add(prefab.name, prefab);
        _trmParents.Add(prefab.name, parent);
        _pools.Add(prefab.name, new Stack<GameObject>());
        for (int i = 0; i < cnt; i++)
        {
            GameObject obj;
            obj = GameObject.Instantiate (_prefabs[prefab.name], _trmParents[prefab.name]);
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
            obj = GameObject.Instantiate(_prefabs[prefabName], _trmParents[prefabName]);
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
