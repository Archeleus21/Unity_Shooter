using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;  //smooths camera during movement

    private Vector3 offset;  //distance between camera and player

    private void Start()
    {
        offset = transform.position - target.position;  //gets distance from camera to player
    }

    private void FixedUpdate()  //fixed used to move camera because we are following a physics object and b/c player is moving in fixedupdate
    {
        Vector3 targetCamPos = target.position + offset;  //stores new cam position

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);  //actually moves camera using Lerp, Lerps smoothly moves object between positions
    }
}
