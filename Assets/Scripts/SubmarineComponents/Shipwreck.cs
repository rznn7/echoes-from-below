using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shipwreck : MonoBehaviour
{
    public void DropItem()
    {
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        
        if (interaction == null)
            return;

       
        if (Random.value < 0.25f)
        {
            int batteryAmountToCharge = Random.Range(10, 50);
            interaction.SendBatteryInteractionMessage(batteryAmountToCharge, () =>
            {
                GameUIManager.updatePower(GameUIManager.instance.power.value + batteryAmountToCharge);
            });
        }
        else if (Random.value > 0.25f && Random.value < 0.5f)
        {
            int oxygenAmountToRecover = Random.Range(10, 50);
            interaction.SendOxygenInteractionMessage(oxygenAmountToRecover, () =>
            {
                GameUIManager.updateOxygen(GameUIManager.instance.oxygen.value + oxygenAmountToRecover);
            });
        }
        else if (Random.value > 0.5f && Random.value < 0.75f)
        {
            int scrapToCollect = Random.Range(1, 4);
            interaction.SendScrapInteractionMessage(scrapToCollect, () =>
            {
                GameUIManager.updateScrap(FindObjectOfType<PlayerStats>().scrapCount + scrapToCollect);
            });
        }
        else
        {
            int ammoToCollect = Random.Range(1, 4);
            interaction.SendAmmoInteractionMessage(ammoToCollect, () =>
            {
                GameUIManager.updateBullets(GameUIManager.instance.bullets.value + ammoToCollect);
            });
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
