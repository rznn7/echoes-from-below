using System;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineTest : MonoBehaviour
{
    public Power power;
    public Oxygen oxygen;
    
    [SerializeField] private Button subtractPowerButton;
    [SerializeField] private Button subtractOxygenButton;
    
    private void Awake()
    {
        power = new Power();
        oxygen = new Oxygen();
    }

    private void Start()
    {
        GameUIManager.initPower(power.MaxAmount);
        GameUIManager.initOxygen(oxygen.MaxAmount);
        
        GameUIManager.instance.power.updateValue(power.CurrentAmount);
        GameUIManager.instance.oxygen.updateValue(oxygen.CurrentAmount);

        subtractPowerButton.onClick.AddListener(SubtractPower);
        subtractOxygenButton.onClick.AddListener(SubtractOxygen);
    }

    private void OnDestroy()
    {
        subtractPowerButton.onClick.RemoveListener(SubtractPower);
        subtractOxygenButton.onClick.RemoveListener(SubtractOxygen);
    }
    
    private void SubtractPower()
    {
        power.SubtractPower(10);
        GameUIManager.instance.power.updateValue(power.CurrentAmount);
        Debug.Log("Power: " + power.CurrentAmount);
    }
    
    private void SubtractOxygen()
    {
        oxygen.SubtractOxygen(10);
        GameUIManager.instance.oxygen.updateValue(oxygen.CurrentAmount);
        Debug.Log("Oxygen: " + oxygen.CurrentAmount);
    }
}
