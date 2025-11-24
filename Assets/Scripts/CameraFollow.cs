using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float acompanha = 0.125f;
    public Vector3 offset;
    public float minY, maxY;
    public float minX, maxX;

    // Start is called before the first frame update
    void Start()
    {

        // Se necessário, inicialize offset aqui
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, acompanha);
        transform.position = smoothedPosition;
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);    
    }
}