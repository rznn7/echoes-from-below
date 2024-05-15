using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shipwreck : MonoBehaviour
{
    //50% chance to drop a battery
    //50% chance to drop an oxygen pack
    
    [SerializeField] private Button dropItemButton;
    
    private Power power;
    private Oxygen oxygen;

    private void Start()
    {
        dropItemButton.onClick.AddListener(DropItem);

        power = FindObjectOfType<SubmarineTest>().power;
        oxygen = FindObjectOfType<SubmarineTest>().oxygen;
        
        if (power != null)
            Debug.Log("Power has been found");
        
        if (oxygen != null)
            Debug.Log("Oxygen has been found");
    }

    private void OnDestroy()
    {
        dropItemButton.onClick.RemoveListener(DropItem);
    }

    private void DropItem()
    {
        if (Random.value < 0.5f)
        {
            Debug.Log("Shipwreck yields a battery");
            Battery battery = new Battery(50);

            if (power != null)
            {
                power.AddPower(battery.AmountToCharge);
                GameUIManager.instance.power.updateValue(power.CurrentAmount);
            }
            else
                Debug.LogWarning("Power is null");
        }
        else
        {
            Debug.Log("Shipwreck yields an oxygen pack");
            OxygenPack oxygenPack = new OxygenPack(50);
            
            if (oxygen != null)
            {
                oxygen.AddOxygen(oxygenPack.AmountToRecover);
                GameUIManager.instance.oxygen.updateValue(oxygen.CurrentAmount);
            }
            else
                Debug.LogWarning("Oxygen is null");
        }
    }
}
