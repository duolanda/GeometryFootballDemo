using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rig;
    Vector3 initPos = Vector3.zero;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void InitBall()
    {
        rig.velocity = Vector3.zero;
        transform.localPosition = initPos;
    }
}
