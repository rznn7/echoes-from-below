using System;
using UnityEngine;

public class EventInteraction : MonoBehaviour
{
    public TextualInteractionManager textualInteractionManager;
    
    public void SendBatteryInteractionMessage(float amountToCharge, Action onAccept)
    {
        var interaction = new TextualInteraction(
            "You come across a shipwreck up ahead. You might find something interesting in it...",
            $"Obtained a battery! Charged {amountToCharge} units of power.",
            "Accept",
            "Deny",
            "Close"
        );

        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                onAccept?.Invoke();
                Debug.Log("Player accepted the interaction");
            }
        });
    }
    
    public void SendOxygenInteractionMessage(float amountToRecover, Action onAccept)
    {
        var interaction = new TextualInteraction(
            "You come across a shipwreck up ahead. You might find something interesting in it...",
            $"Obtained an oxygen pack! Recovered {amountToRecover} units of oxygen.",
            "Accept",
            "Deny",
            "Close"
        );

        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                onAccept?.Invoke();
                Debug.Log("Player accepted the interaction");
            }
        });
    }
    
    public void SendScrapInteractionMessage(int amountToAdd, Action onAccept)
    {
        var interaction = new TextualInteraction(
            "You come across a shipwreck up ahead. You might find something interesting in it...",
            $"Obtained {amountToAdd} scrap!",
            "Accept",
            "Deny",
            "Close"
        );
        
        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                onAccept?.Invoke();
                Debug.Log("Player accepted the interaction");
            }
        });
    }
    
    public void SendAmmoInteractionMessage(int amountToAdd, Action onAccept)
    {
        var interaction = new TextualInteraction(
            "You come across a shipwreck up ahead. You might find something interesting in it...",
            $"Obtained {amountToAdd} bullets!",
            "Accept",
            "Deny",
            "Close"
        );
        
        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                onAccept?.Invoke();
                Debug.Log("Player accepted the interaction");
            }
        });
    }
}
