using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Action<bool> OnSelection;

    public void OnSelect(BaseEventData eventData)
    {
        OnSelection?.Invoke(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnSelection?.Invoke(false);
    }
}
