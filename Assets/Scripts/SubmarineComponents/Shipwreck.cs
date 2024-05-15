using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace SubmarineComponents
{
    public class Shipwreck : MonoBehaviour
    {
        //50% chance to drop a battery
        //50% chance to drop an oxygen pack
        
        [SerializeField] private Button dropItemButton;
        
        private Power power;

        private void Start()
        {
            dropItemButton.onClick.AddListener(DropItem);

            power = FindObjectOfType<SubmarineTest>().power;
            
            if (power != null)
                Debug.Log("Power has been found");
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
                Battery battery = new Battery();

                if (power != null)
                {
                    power.AddPower(battery.AmountToCharge);
                    Debug.Log(power.CurrentAmount);
                }
                else
                    Debug.LogWarning("Power is null");
            }
            else
            {
                Debug.Log("Shipwreck yields an oxygen pack");
            }
        }
    }
}