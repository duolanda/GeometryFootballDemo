using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public Transform trackPlayer;
    float initialPositionZ;
    void Start()
    {
        initialPositionZ = transform.position.z;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(trackPlayer.position.x, transform.position.y, trackPlayer.position.z + initialPositionZ);
        if(transform.position.z >= -7.5)
        {
            transform.position = new Vector3(trackPlayer.position.x, transform.position.y, -7.5f);
        }
        else if (transform.position.z <= -14.5)
        {
            transform.position = new Vector3(trackPlayer.position.x, transform.position.y, -14.5f);
        }
    }
}
