using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraFollow2 : MonoBehaviour
{
    [Header("Referência")]
    public Transform player;

    [Header("Suavização")]
    public float acompanha = 0.125f;
    public Vector3 offset = Vector3.zero;

    [Header("Limites do MAPA (esquerda, direita, baixo, cima)")]
    public float minX = -15f;
    public float maxX = 31f;
    public float minY = -10f;
    public float maxY = 13f;


    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 desired = player.position + offset;

        Vector3 smoothed = Vector3.Lerp(transform.position, desired, acompanha);

        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.orthographicSize * cam.aspect;

        float clampedX = Mathf.Clamp(smoothed.x, minX + halfWidth, maxX - halfWidth);
        float clampedY = Mathf.Clamp(smoothed.y, minY + halfHeight, maxY - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
