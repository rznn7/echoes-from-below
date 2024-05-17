using UnityEngine;

public class MapCameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
    }
}
