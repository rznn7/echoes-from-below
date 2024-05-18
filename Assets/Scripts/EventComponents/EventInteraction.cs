using System;
using UnityEngine;

public class EventInteraction : MonoBehaviour
{
    public TextualInteractionManager textualInteractionManager;
    
    #region Battery
    public void SendBatteryFromShipwreckInteractionMessage(float amountToCharge, Action onAccept, Action onDeny)
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
            }
            else
            {
                onDeny?.Invoke();
            }
        });
    }
    #endregion
    
    #region Oxygen
    public void SendOxygenFromShipwreckInteractionMessage(float amountToRecover, Action onAccept, Action onDeny)
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
            }
            else
            {
                onDeny?.Invoke();
            }
        });
    }
    #endregion
    
    #region Scrap
    public void SendScrapFromShipwreckInteractionMessage(int amountToAdd, Action onAccept, Action onDeny)
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
            }
            else
            {
                onDeny?.Invoke();
            }
        });
    }

    public void SendScrapInteractionMessage(int amountToAdd, Action onAccept, Action onDeny)
    {
        var interaction = new TextualInteraction(
            "You see some scrap floating around. Do you want to pick it up?",
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
            }
            else
            {
                onDeny?.Invoke();
            }
        });
    }
    #endregion
    
    #region Ammo
    public void SendAmmoFromShipwreckInteractionMessage(int amountToAdd, Action onAccept, Action onDeny)
    {
        var interaction = new TextualInteraction(
            "You come across a shipwreck up ahead. You might find something interesting in it...",
            amountToAdd > 1 ? $"Obtained {amountToAdd} bullets!" : $"Obtained {amountToAdd} bullet!",
            "Accept",
            "Deny",
            "Close"
        );

        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                onAccept?.Invoke();
            }
            else
            {
                onDeny?.Invoke();
            }
        });
    }
    #endregion
    
    #region Shoal of Fish

    public void SendFishShoalInteractionMessage(Action onAccept, Action onDeny)
    {
        var interaction = new TextualInteraction(
            "You observe a shoal of fish swimming around.",
            $"You feel a little better amidst the loneliness of the deep sea.",
            "Ponder",
            "Keep Going",
            "Close"
        );
        
        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                onAccept?.Invoke();
            }
            else
            {
                onDeny?.Invoke();
            }
        });
    }
    #endregion
    
    #region Sea Mine
    public void SendSeaMineInteractionMessage(bool explode, Action onAccept, Action onDeny)
    {
        var interaction = new TextualInteraction(
            "While navigating your submarine, you find that you're in proximity to a sea mine.",
            explode
                ? "The mine explodes! Your submarine has taken damage!"
                : "You get closer, but nothing happens. Must be a fake.",
            "Accept",
            "Deny",
            "Close"
        );
        
        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                onAccept?.Invoke();
            }
            else
            {
                onDeny?.Invoke();
            }
        });
    }
    #endregion
}
