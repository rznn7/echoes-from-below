using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scrap : MonoBehaviour, IHandleInteraction 
{
    public IEnumerator HandleEventInteraction()
    {
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        if (interaction == null) yield break;
        
        GameUIManager.eventDisp(3);

        bool eventHandled = false;
        
        Action onAccept = () => 
        {
            eventHandled = true;
        };

        Action onDeny = () =>
        {
            eventHandled = true;
        };
        
        int scrapToCollect = Random.Range(1, 4);
        interaction.SendScrapInteractionMessage(scrapToCollect, 
            () => {
                onAccept.Invoke();
                GameUIManager.updateScrap(FindObjectOfType<PlayerStats>().scrapCount + scrapToCollect);
            }, onDeny);
        
        while (!eventHandled)
        {
            yield return null;
        }

        GameUIManager.eventDisp(-1);
        gameObject.SetActive(false);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
