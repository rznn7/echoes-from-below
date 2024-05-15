using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SliderDisplay : MonoBehaviour
{
    public TMP_Text display;
    public void onSliderUpdate(float n) {
        display.text = "" + n;
    }
}
