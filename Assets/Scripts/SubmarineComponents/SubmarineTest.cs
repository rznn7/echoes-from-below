using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SubmarineComponents
{
    public class SubmarineTest : MonoBehaviour
    {
        public Power power;
        
        [SerializeField] private Button subtractPowerButton;
        [SerializeField] private TextMeshProUGUI currentPowerText;
        
        private void Awake()
        {
            power = new Power();
        }

        private void Start()
        {
            Debug.Log(power.MaxAmount);
            Debug.Log(power.CurrentAmount);

            UpdateText();
            
            subtractPowerButton.onClick.AddListener(SubtractPower);
        }

        private void OnDestroy()
        {
            subtractPowerButton.onClick.RemoveListener(SubtractPower);
        }
        
        private void SubtractPower()
        {
            power.SubtractPower(10);
            UpdateText();
            Debug.Log(power.CurrentAmount);
        }
        
        private void UpdateText()
        {
            currentPowerText.text = "Power: " + power.CurrentAmount;
        }
    }
}