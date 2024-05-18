using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shipwreck : MonoBehaviour
{
    public IEnumerator HandleEventInteraction()
    {
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        if (interaction == null) yield break;

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

        if (Random.value < 0.25f)
        {
            int batteryAmountToCharge = Random.Range(10, 50);
            interaction.SendBatteryInteractionMessage(batteryAmountToCharge, onAccept, onDeny);
        }
        else if (Random.value < 0.5f)
        {
            int oxygenAmountToRecover = Random.Range(10, 50);
            interaction.SendOxygenInteractionMessage(oxygenAmountToRecover, onAccept, onDeny);
        }
        else if (Random.value < 0.75f)
        {
            int scrapToCollect = Random.Range(1, 4);
            interaction.SendScrapInteractionMessage(scrapToCollect, onAccept, onDeny);
        }
        else
        {
            int ammoToCollect = Random.Range(1, 4);
            interaction.SendAmmoInteractionMessage(ammoToCollect, onAccept, onDeny);
        }

        while (!eventHandled)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
