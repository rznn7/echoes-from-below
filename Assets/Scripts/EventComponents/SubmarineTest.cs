using System;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineTest : MonoBehaviour
{
    [SerializeField] private Button subtractPowerButton;
    [SerializeField] private Button subtractOxygenButton;

    private void Start()
    {
        GameUIManager.initPower(GameUIManager.instance.power.max);
        GameUIManager.initOxygen(GameUIManager.instance.oxygen.max);

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
        GameUIManager.updatePower(GameUIManager.instance.power.value - 10);
    }
    
    private void SubtractOxygen()
    {
        GameUIManager.updateOxygen(GameUIManager.instance.oxygen.value - 10);
    }
}
