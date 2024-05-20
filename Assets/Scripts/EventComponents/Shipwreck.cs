using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shipwreck : MonoBehaviour, IHandleInteraction
{
    public IEnumerator HandleEventInteraction()
    {
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        if (interaction == null) yield break;
        
        GameUIManager.EventDisp(1);

        bool eventHandled = false;

        Action onAccept = () => 
        {
            eventHandled = true;
        };

        Action onDeny = () =>
        {
            eventHandled = true;
        };

        if (Random.value < 0.33f)
        {
            int batteryAmountToCharge = Random.Range(10, 50);
            interaction.SendBatteryFromShipwreckInteractionMessage(batteryAmountToCharge, 
                () => {
                    onAccept.Invoke();
                    GameUIManager.UpdatePower(GameUIManager.instance.power.value + batteryAmountToCharge);
                }, onDeny);
        }
        else if (Random.value < 0.66f)
        {
            int oxygenAmountToRecover = Random.Range(10, 50);
            interaction.SendOxygenFromShipwreckInteractionMessage(oxygenAmountToRecover, 
                () => {
                    onAccept.Invoke();
                    GameUIManager.UpdateOxygen(GameUIManager.instance.oxygen.value + oxygenAmountToRecover);
                }, onDeny);
        }
        else if (Random.value < 0.75f)
        {
            int scrapToCollect = Random.Range(1, 4);
            interaction.SendScrapFromShipwreckInteractionMessage(scrapToCollect, 
                () => {
                    onAccept.Invoke();
                    GameUIManager.UpdateScrap(FindObjectOfType<PlayerStats>().scrapCount + scrapToCollect);
                }, onDeny);
        }
        else
        {
            int ammoToCollect = Random.Range(1, 4);
            interaction.SendAmmoFromShipwreckInteractionMessage(ammoToCollect, 
                () => {
                    onAccept.Invoke();
                    GameUIManager.UpdateBullets(GameUIManager.instance.bullets.value + ammoToCollect);
                }, onDeny);
        }

        while (!eventHandled)
        {
            yield return null;
        }

        GameUIManager.EventDisp(-1);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
