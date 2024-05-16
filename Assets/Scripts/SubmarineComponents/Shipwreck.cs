using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shipwreck : MonoBehaviour, IPopupActivator
{
    public event Action ActivatePopup;
    
    [SerializeField] private Button dropItemButton;
    
    private void Start()
    {
        dropItemButton.onClick.AddListener(DropItem);
    }

    private void OnDestroy()
    {
        dropItemButton.onClick.RemoveListener(DropItem);
    }

    private void DropItem()
    {
        if (Random.value < 0.5f)
        {
            ShipwreckPopupSender popupSender = FindObjectOfType<ShipwreckPopupSender>();
            
            Battery battery = new Battery(Random.Range(10, 50));
            
            if (popupSender != null)
                popupSender.SendBatteryPopupMessage(battery.AmountToCharge);
            ActivatePopup?.Invoke();
            
            GameUIManager.updatePower(GameUIManager.instance.power.value + battery.AmountToCharge);
            Debug.Log("Power: " + GameUIManager.instance.power.value);
        }
        else
        {
            ShipwreckPopupSender popupSender = FindObjectOfType<ShipwreckPopupSender>();
            
            OxygenPack oxygenPack = new OxygenPack(Random.Range(10, 50));
            
            if (popupSender != null)
                popupSender.SendOxygenPopupMessage(oxygenPack.AmountToRecover);
            ActivatePopup?.Invoke();
            
            GameUIManager.updateOxygen(GameUIManager.instance.oxygen.value + oxygenPack.AmountToRecover);
            Debug.Log("Oxygen: " + GameUIManager.instance.oxygen.value);
        }
    }
}
