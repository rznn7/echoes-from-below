using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shipwreck : MonoBehaviour
{
    public void DropItem()
    {
        ShipwreckInteraction interaction = FindObjectOfType<ShipwreckInteraction>();
        
        if (interaction == null)
            return;

        bool isBattery = Random.value < 0.5f;
        if (isBattery)
        {
            Battery battery = new Battery(Random.Range(10, 50));
            interaction.SendBatteryInteractionMessage(battery.AmountToCharge);
            GameUIManager.updatePower(GameUIManager.instance.power.value + battery.AmountToCharge);
        }
        else
        {
            OxygenPack oxygenPack = new OxygenPack(Random.Range(10, 50));
            interaction.SendOxygenInteractionMessage(oxygenPack.AmountToRecover);
            GameUIManager.updateOxygen(GameUIManager.instance.oxygen.value + oxygenPack.AmountToRecover);
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
