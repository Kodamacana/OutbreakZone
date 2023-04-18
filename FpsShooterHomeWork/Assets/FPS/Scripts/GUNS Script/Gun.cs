using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX; 

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public VisualEffect muzzleFlash;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            muzzleFlash.SendEvent("OnStop");
        }
    }
    void Shoot()
    {
        muzzleFlash.gameObject.SetActive(true);
        muzzleFlash.SendEvent("OnPlay");

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
