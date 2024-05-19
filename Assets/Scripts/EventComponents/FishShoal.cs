using System;
using System.Collections;
using UnityEngine;

public class FishShoal : MonoBehaviour, IHandleInteraction
{
    private bool interactable = true;
    
    public IEnumerator HandleEventInteraction()
    {
        if (!interactable) yield break;
        
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        if (interaction == null) yield break;
        
        GameUIManager.EventDisp(2);
        
        bool eventHandled = false;
        
        Action onAccept = () =>
        {
            eventHandled = true;
        };
        
        Action onDeny = () =>
        {
            eventHandled = true;
        };
        
        interaction.SendFishShoalInteractionMessage(
            () => { onAccept.Invoke(); }, onDeny);
        
        while (!eventHandled)
        {
            yield return null;
        }

        GameUIManager.EventDisp(-1);
        interactable = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}