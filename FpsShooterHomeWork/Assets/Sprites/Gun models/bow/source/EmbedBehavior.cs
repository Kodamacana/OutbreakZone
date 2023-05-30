using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbedBehavior : MonoBehaviour
{
    Rigidbody rigidB;    

    private void Start()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.transform.GetComponent<Target>();

        if (collision.transform.CompareTag("Enemy"))
            target.TakeDamage(45);
    }
    private void OnTriggerEnter(Collider other)
    {
        Embed();
    }

    void Embed()
    {   
        transform.GetComponent<BowProjectileAddForce>().enabled = false;
        rigidB.velocity = Vector3.zero;
        rigidB.useGravity = false;
        rigidB.isKinematic = true;
    }
}
