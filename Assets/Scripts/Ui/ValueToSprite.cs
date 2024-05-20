using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ValueToSprite : MonoBehaviour, IPopupMessageSender
{
    public float max;
    public float min;
    public float value;
    public Sprite[] sprites;
    public Image img;
    private float slabSize;

    public event Action<List<PopupMessage>, MessagePriority> AddMessagesToQueue;
    List<PopupMessage> _popupMessage;
    private void Start()
    {
        recalculateSlabSize();
        updateDisplay();
    }
    public void updateValue(float a,string item) {

        if (a > max) {
            _popupMessage = new List<PopupMessage>();
            _popupMessage.Add(new PopupMessage($"Dropped {(a-max)} {item}s since your {item} storage is full.", "OK"));
            AddMessagesToQueue?.Invoke(_popupMessage, MessagePriority.Medium);
        }
        value = Mathf.Clamp(a, min, max);
        updateDisplay();
    }
    public void updateDisplay() {
        if (value >= max)
        {
            img.sprite = sprites[sprites.Length - 1];
        }
        else if (value <= min)
        {
            img.sprite = sprites[0];
        }
        else
        {
            if (value < 0) { value = 0; }
            if (value > max) { value =max; }

            int number = Mathf.FloorToInt(value / slabSize);
            print(number);
            print(this.gameObject.name);
            img.sprite = sprites[number];
        }
    }
    void recalculateSlabSize() {
        slabSize = (max - min) / sprites.Length;
    }
}
