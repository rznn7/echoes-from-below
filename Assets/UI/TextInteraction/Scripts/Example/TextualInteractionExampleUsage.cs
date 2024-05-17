using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextualInteractionExampleUsage : MonoBehaviour
{
    public TextualInteractionManager textualInteractionManager;

    void Start()
    {
        // Setup interaction variables
        var ammoFound = Random.Range(0, 2); // Generate 0 or 1
        var batteryFound = Random.Range(0, 3); // Generate 0, 1, or 2

        // Setup interactionEndText (text displayed if the interaction is accepted)
        var interactionEndText = GetInteractionEndText(ammoFound, batteryFound);

        var interaction = new TextualInteraction(
            "You found an old shipwreck. It seems dangerous to search it but you could find something interesting...",
            interactionEndText,
            "Search the ship",
            "Leave it",
            "OK"
        );

        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                Debug.Log("Player accepted the interaction.");
                // Actions triggered by acceptance (e.g., add ammunition and batteries to stock)
                ProcessFoundItems(ammoFound, batteryFound);
            }
            else
            {
                Debug.Log("Player denied the interaction.");
            }
        });
    }

    string GetInteractionEndText(int ammoFound, int batteryFound)
    {
        if (ammoFound == 0 && batteryFound == 0)
        {
            return "You found nothing.";
        }

        var interactionEndText = "You found: ";
        if (ammoFound > 0)
        {
            interactionEndText += $"\n- Ammunition ({ammoFound})";
        }
        if (batteryFound > 0)
        {
            interactionEndText += $"\n- Battery ({batteryFound})";
        }

        return interactionEndText;
    }

    void ProcessFoundItems(int ammoFound, int batteryFound)
    {
        // Implement logic to add items to player's inventory
        if (ammoFound > 0)
        {
            // Add ammoFound to inventory
        }
        if (batteryFound > 0)
        {
            // Add batteryFound to inventory
        }
    }
}
