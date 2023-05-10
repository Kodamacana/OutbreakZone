using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX; 

public class Gun : MonoBehaviour
{
    //public float damage;
    //public float range = 100f;

    //public Camera fpsCam;
    //public VisualEffect muzzleFlash;
    //[SerializeField] float ShootDelay;

    //bool isFire = true;

    //void Update()
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        if (ShootDelay != 0)
    //        {
    //            InvokeRepeating("Shoot", 0f, ShootDelay);
    //        }
    //        else if(isFire)
    //        {
    //            Shoot();
    //            isFire = false;
    //            Invoke("stopMuzzleFlash", 0.2f);
    //        }
    //    }
    //    else if (Input.GetButtonUp("Fire1"))
    //    {
    //        CancelInvoke("Shoot");
    //        muzzleFlash.SendEvent("OnStop");
    //    }
    //}

    //void stopMuzzleFlash()
    //{
    //    isFire = true;
    //    muzzleFlash.SendEvent("OnStop");
    //}
    //void Shoot()
    //{
    //    muzzleFlash.gameObject.SetActive(true);
    //    muzzleFlash.SendEvent("OnPlay");
    //    RaycastHit hit;
    //    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
    //    {
    //        Debug.Log(hit.transform.name);

    //        Target target = hit.transform.GetComponent<Target>();
    //        if (target != null)
    //        {
    //            target.TakeDamage(damage);
    //        }
    //    }
    //}


    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public VisualEffect muzzleFlash;
    public GameObject bulletHoleGraphic;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        
        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        if (shooting && !allowButtonHold)
        {
            Invoke("stopMuzzleFlash", 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            muzzleFlash.gameObject.SetActive(true);
            muzzleFlash.SendEvent("OnPlay");
            bulletsShot = bulletsPerTap;
            Shoot();
        }
        else if (allowButtonHold && !shooting)
        {
            Invoke("stopMuzzleFlash", 0.1f);
        }
    }

    void stopMuzzleFlash()
    {
        muzzleFlash.gameObject.SetActive(false);
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            Target target = rayHit.transform.GetComponent<Target>();

            if (rayHit.collider.CompareTag("Enemy"))
                target.TakeDamage(damage);
        }


        //Graphics
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
