using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ValueToSprite : MonoBehaviour
{
    public float max;
    public float min;
    public float value;
    public Sprite[] sprites;
    public Image img;
    private float slabSize;
    private void Start()
    {
        recalculateSlabSize();
        updateDisplay();
    }
    public void updateValue(float a) {
        value = a;
        updateDisplay();
    }
    public void updateDisplay() {
        int number = Mathf.FloorToInt(value/slabSize);
        if (number == 0) {
            number++;
        }
        img.sprite = sprites[number-1];
          
    }
    void recalculateSlabSize() {
        slabSize = (max - min) / sprites.Length;
    }
}
