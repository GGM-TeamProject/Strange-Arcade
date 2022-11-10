using UnityEngine.Events;

public interface IDamage
{
    public void OnDamage(float damage, UnityEvent CallBack = null);
}
