using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leakData : MonoBehaviour
{
    public float max;
    public float value;
    public GameObject[] leaks;
    private List<GameObject> deactivated;
    private List<GameObject> activated;
    public void updateValue(float v) {
        value = v;
        if (value <= 20) {
            if (activated.Count > 0) {
                while (activated.Count > 0) {
                    activated[0].SetActive(false);
                    deactivated.Add(activated[0]);
                    activated.RemoveAt(0);
                }
            }
        }
        else if (value is > 20 and <= 50)
        {
            if (activated.Count < 1)
            {
                while (activated.Count < 1)
                {
                    deactivated[0].SetActive(true);
                    activated.Add(deactivated[0]);
                    deactivated.RemoveAt(0);
                    
                }
            }
        }
        else if (value is > 50 and <= 70)
        {
            if (activated.Count < 2)
            {
                while (activated.Count < 2)
                {
                    deactivated[0].SetActive(true);
                    activated.Add(deactivated[0]);
                    deactivated.RemoveAt(0);
                }
            }
        }
        else if (value > 70)
        {
            if (activated.Count < 3)
            {
                while (activated.Count < 3)
                {
                    deactivated[0].SetActive(true);
                    activated.Add(deactivated[0]);
                    deactivated.RemoveAt(0);
                }
            }
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        deactivated = new List<GameObject>(leaks);
        activated = new List<GameObject>();
    }
    public int getActiveNumber() {
        return activated.Count;    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
