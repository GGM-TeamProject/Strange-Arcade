using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage3_Car : MonoBehaviour, IDamage
{
    public void OnDamage(float damage, UnityEvent CallBack = null)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Item")){
            Iitem item = other.transform.GetComponent<Iitem>();
            item?.OnUseItem();
        }
    }
}
