using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHolder : MonoBehaviour
{

    public static float SFXvol = 1f;
    public static float Musvol = 0.3f;
    public AudioSource Music;
    // Start is called before the first frame update
    void Start()
    {
        SFXvol = 1;
        Musvol = 0.575f;
    }

    // Update is called once per frame
    void Update()
    {
        Music.volume = Musvol;
    }
}
