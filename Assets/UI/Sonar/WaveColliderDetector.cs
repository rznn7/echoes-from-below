using UnityEngine;

public class WaveCollisionDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<SonarListener>(out var sonarReceptor))
        {
            sonarReceptor.OnActivate();
        }
    }
}
