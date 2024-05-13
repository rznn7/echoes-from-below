using System;
using UnityEngine;

public class ExampleActivator : MonoBehaviour, IPopupActivator
{
    public event Action ActivatePopup;

    public void SendActivationEvent()
    {
        ActivatePopup?.Invoke();
        Debug.Log("Clicked on message button in cockpit");
    }
}
