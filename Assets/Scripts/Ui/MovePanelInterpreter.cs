using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovePanelInterpreter : MonoBehaviour
{
    public Image img;
    public Sprite non;
    public Sprite[] icons;
    public int movchoice = -1;
    public void updateImg(int a)
    {
        if (movchoice == a) {
            img.sprite = non;
            movchoice = -1;
            return;
        }
        img.sprite = icons[a];
        movchoice = a;
    }
}
