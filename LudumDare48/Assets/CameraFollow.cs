using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerMovement player;
    public float smoothSpeed = 2f;
    public float offsetX = 4;
    public float offsetY = 0;
    public float MaxX;
    public float MinX;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = player.gameObject.transform;
    }

    private Vector3 newPos;
    private Vector3 smoothPos;
    float xPos;


    void LateUpdate()
    {
        xPos = target.position.x + offsetX; /** (player.lookRight ? 1 : -1);*/

        newPos = new Vector3(
                Mathf.Clamp(xPos, MinX, MaxX),
                target.position.y + offsetY,
                transform.position.z);
        smoothPos = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothPos;

    }
}