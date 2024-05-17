using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TextualInteractionExampleUsage : MonoBehaviour
{
    public TextualInteractionManager textualInteractionManager;

    void Start()
    {
        // Setup interaction variables
        var ammoFound = Random.Range(0, 1);
        var batteryFound = Random.Range(0, 2);

        // Setup interactionEndText (text displayed if the interaction is accepted)
        string interactionEndText;

        if (ammoFound == 0 && batteryFound == 0)
        {
            interactionEndText = "You found nothing.";
        }
        else
        {
            interactionEndText = "You found: ";
            if (ammoFound > 0)
            {
                interactionEndText += $"\n- Ammunition ({ammoFound})";
            }
            if (batteryFound > 0)
            {
                interactionEndText += $"\n- Battery ({batteryFound})";
            }
        }

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
                // Actions triggered by acceptation (In this example add ammunition and batteries to stock)
            }
            else
            {
                Debug.Log("Player denied the interaction.");
            }
        });
    }
}
