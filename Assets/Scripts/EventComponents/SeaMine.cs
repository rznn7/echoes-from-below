using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeaMine : MonoBehaviour, IHandleInteraction
{
    public event Action OnMineExploded;
    private static bool firstMineEncounter = true;
    
    public IEnumerator HandleEventInteraction()
    {
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        if (interaction == null) yield break;
        
        GameUIManager.eventDisp(0);

        bool eventHandled = false;
        
        Action onAccept = () => 
        {
            eventHandled = true;
        };

        Action onDeny = () =>
        {
            eventHandled = true;
        };

        bool explode;

        if (firstMineEncounter)
        {
            explode = false;
            firstMineEncounter = false;
        }
        else
        {
            explode = Random.value < 0.5f;
        }

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
        float leakIncrease = Random.Range(30, 60);
        GameUIManager.updateLeak(GameUIManager.instance.leak.value + leakIncrease);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
