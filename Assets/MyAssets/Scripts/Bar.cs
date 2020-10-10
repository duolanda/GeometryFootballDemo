using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    Transform center;
    public float shootForce = 100f;

    void Start()
    {
        center = transform.parent;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Transform targetBall = collision.transform;
            Rigidbody ballRig = targetBall.GetComponent<Rigidbody>();
            Vector3 direction = targetBall.position - center.position;
            direction.y = 0;
            direction = direction.normalized;
            //Debug.DrawRay(targetBall.position, direction * shootForce, Color.red, 0.5f);
            ballRig.AddForce(direction * shootForce, ForceMode.Impulse);
        }   
    }

}
