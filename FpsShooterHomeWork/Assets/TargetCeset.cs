using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCeset : MonoBehaviour
{
    [SerializeField] GameObject Blood;
    public void TakeDamageCeset()
    {
        Instantiate(Blood, transform.position, Quaternion.identity);
    }
}
