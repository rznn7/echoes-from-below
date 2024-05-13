using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAutoBlink : MonoBehaviour
{
    public Vector2 range;
    private float timer;
    private float duration;
    public GameObject blinker;
    // Start is called before the first frame update
    void Start()
    {
        duration = Random.Range(range.x, (float)range.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration) {
            timer = 0;
            duration = Random.Range(range.x, (float)range.y);
            blinker.SetActive(!blinker.activeSelf);
        }
    }
}
