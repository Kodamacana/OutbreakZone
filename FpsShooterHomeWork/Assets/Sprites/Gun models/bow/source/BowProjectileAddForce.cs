using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowProjectileAddForce : MonoBehaviour
{
    Rigidbody rigidB;
    public float shootForce = 500;

    private void OnEnable()
    {
        rigidB = GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.zero;
        ApplyForce();
    }

    void ApplyForce()
    {
        rigidB.AddRelativeForce(Vector3.down * shootForce);
    }

    void SpinObjectInAir()
    {

    }

    void Update()
    {
        SpinObjectInAir();
    }
}
