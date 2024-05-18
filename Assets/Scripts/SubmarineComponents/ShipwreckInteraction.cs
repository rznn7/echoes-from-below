using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipwreckInteraction : MonoBehaviour
{
    public TextualInteractionManager textualInteractionManager;
    
    public void SendBatteryInteractionMessage(float amountToCharge)
    {
        var interaction = new TextualInteraction(
            "You come across a shipwreck. You might find something interesting in it...",
            $"Obtained a battery! Charged {amountToCharge} units of power.",
            "Accept",
            "Deny",
            "Close"
        );

        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                Debug.Log("Player accepted the interaction");
            }
        });
    }
    
    public void SendOxygenInteractionMessage(float amountToRecover)
    {
        var interaction = new TextualInteraction(
            "You come across a shipwreck. You might find something interesting in it...",
            $"Obtained an oxygen pack! Recovered {amountToRecover} units of oxygen.",
            "Accept",
            "Deny",
            "Close"
        );

        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                Debug.Log("Player accepted the interaction");
            }
        });
    }
}
