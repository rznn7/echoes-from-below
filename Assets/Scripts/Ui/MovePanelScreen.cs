using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovePanelScreen : MonoBehaviour
{
    public Image img;

    public void updateImg(Sprite s) {
        img.sprite = s;
    }
}
