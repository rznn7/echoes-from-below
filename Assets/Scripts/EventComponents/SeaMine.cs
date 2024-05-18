using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeaMine : MonoBehaviour, IHandleInteraction
{
    public event Action OnMineExploded;
    
    public IEnumerator HandleEventInteraction()
    {
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        if (interaction == null) yield break;
        
        GameUIManager.eventDisp(0);

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

        bool explode = Random.value < 0.5f;

        if (explode)
        {
            OnMineExploded?.Invoke();
            interaction.SendSeaMineInteractionMessage(true, 
                () => {
                    onAccept.Invoke();
                    DealDamage();
                }, onDeny);
        }
        else
        {
            interaction.SendSeaMineInteractionMessage(false, onAccept, onDeny);
        }
        
        while (!eventHandled)
        {
            yield return null;
        }

        GameUIManager.eventDisp(-1);
        gameObject.SetActive(false);
    }
    
    private void DealDamage()
    {
        //leak
        GameUIManager.updateLeak(GameUIManager.instance.leak.value + Random.Range(30, 60));
        Debug.Log(GameUIManager.instance.leak.value);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
