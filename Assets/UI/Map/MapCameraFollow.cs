using System;
using UnityEngine;

public class MapCameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    Camera _mapCamera;

    void Awake()
    {
        _mapCamera = GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
    }
}
