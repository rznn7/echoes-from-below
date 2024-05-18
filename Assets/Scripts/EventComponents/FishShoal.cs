﻿using System;
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
        
        GameUIManager.eventDisp(2);
        
        bool eventHandled = false;
        
        Action onAccept = () =>
        {
            Debug.Log("Player accepted the interaction");
            eventHandled = true;
        };
        
        Action onDeny = () =>
        {
            Debug.Log("Player denied the interaction");
            eventHandled = true;
        };
        
        interaction.SendFishShoalInteractionMessage(
            () => { onAccept.Invoke(); }, onDeny);
        
        while (!eventHandled)
        {
            yield return null;
        }

        GameUIManager.eventDisp(-1);
        interactable = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}