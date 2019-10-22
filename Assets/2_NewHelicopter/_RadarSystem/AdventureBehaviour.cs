using System;
using UnityEngine;
using System.Collections;

public class AdventureBehaviour : MonoBehaviour
{
    public Action<AdventureBehaviour> OnEnableAction;
    public Action<AdventureBehaviour> OnDisableAction;
    public Action<AdventureBehaviour> OnDestroyAction;

    protected virtual void OnEnableObject()
    {
        if (OnEnableAction != null)
            OnEnableAction(this);
    }

    protected virtual void OnDisableObject()
    {
        if (OnDisableAction != null)
            OnDisableAction(this);
    }

    protected virtual void OnDestroyObject()
    {
        if (OnDestroyAction != null)
            OnDestroyAction(this);
    }

    private void OnEnable()
    {
        OnEnableObject();
    }

    private void OnDisable()
    {
        OnDisableObject();
    }

    private void OnDestroy()
    {
        OnDestroyObject();
    }
}
