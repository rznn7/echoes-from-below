using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckPopupSender : MonoBehaviour, IPopupMessageSender 
{
    public event Action<List<PopupMessage>, MessagePriority> AddMessagesToQueue;
    
    public void SendBatteryPopupMessage(float amount)
    {
        List<PopupMessage> popupMessages = new()
        {
            new PopupMessage(
                "You come across a shipwreck. You might find something interesting in it...", 
                "OK"),
            new PopupMessage(
                $"You found a battery in the shipwreck. Refilled {amount} units of power.",
                "OK")
        };
        
        AddMessagesToQueue?.Invoke(popupMessages, MessagePriority.Medium);
    }

    public void SendOxygenPopupMessage(float amount)
    {
        List<PopupMessage> popupMessages = new()
        {
            new PopupMessage(
                "You come across a shipwreck. You might find something interesting in it...", 
                "OK"),
            new PopupMessage(
                $"You found an oxygen pack in the shipwreck. Recovered {amount} units of oxygen.",
                "OK")
        };
        
        AddMessagesToQueue?.Invoke(popupMessages, MessagePriority.Medium);
    }
}
