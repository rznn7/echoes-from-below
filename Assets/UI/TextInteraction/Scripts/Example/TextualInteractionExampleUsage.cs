using System;
using UnityEngine;

public class TextualInteractionExampleUsage : MonoBehaviour
{
    public TextualInteractionManager textualInteractionManager;

    void Start()
    {
        var interaction = new TextualInteraction(
            "Do you want to start the quest?",
            "Thank you for accepting the quest!",
            "Accept",
            "Deny",
            "Close"
        );

        textualInteractionManager.StartInteraction(interaction).Subscribe((interactionAnswer) =>
        {
            if (interactionAnswer)
            {
                Debug.Log("Player accepted the interaction.");
            }
            else
            {
                Debug.Log("Player denied the interaction.");
            }
        });
    }
}
