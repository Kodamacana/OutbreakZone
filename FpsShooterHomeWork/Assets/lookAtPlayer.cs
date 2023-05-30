using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    GameObject obj;
    private void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(obj.transform.position- transform.position);
    }
}
