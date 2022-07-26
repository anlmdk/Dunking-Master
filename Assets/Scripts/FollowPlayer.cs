using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    [SerializeField] public Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.03f;

    void FixedUpdate()
    {
        Follow();
    }
    public void Follow()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(new Vector3(transform.position.x,0,transform.position.z), desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
