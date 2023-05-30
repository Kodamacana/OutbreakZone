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
        float _yVelocity = rigidB.velocity.y;
        float _zVelocity = rigidB.velocity.z;
        float _xVelocity = rigidB.velocity.x;
        float _combinedVelocity = Mathf.Sqrt(_xVelocity * _xVelocity + _zVelocity * _zVelocity);
        float _fallAngle = -1 * Mathf.Atan2(_yVelocity, _combinedVelocity) * 180 / Mathf.PI;

        transform.eulerAngles = new Vector3(_fallAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void Update()
    {
        SpinObjectInAir();
    }
}
