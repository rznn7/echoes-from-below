using UnityEngine;

public class WaveCollisionDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected: " + other.gameObject.name);
    }
}
