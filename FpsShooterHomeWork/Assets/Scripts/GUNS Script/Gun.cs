using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX; 

public class Gun : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    [SerializeField] Animator GunAnim;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public VisualEffect _muzzleFlash;
    public VisualEffect _brustPrefab;
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
        HandleShootingInput();
        
        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);

        
    }
    private void HandleShootingInput()
    {
        if (readyToShoot)
        {
            bool shooting = allowButtonHold ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);
            bool canShoot = readyToShoot && shooting && !reloading && bulletsLeft > 0;

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            {
                Reload();
            }

            if (canShoot)
            {
                bulletsShot = bulletsPerTap;
                Shoot();
            }


            //if (bulletsLeft <= 0)
            //{
            //    _muzzleFlash.SendEvent("OnPlay");
            //}
            //if (reloading)
            //{
            //   _muzzleFlash.SendEvent("OnPlay");
            //}
            //if (!allowButtonHold && !shooting && !reloading && bulletsLeft > 0)
            //{
            //    _muzzleFlash.SendEvent("OnPlay");
            //}

            //if (allowButtonHold && !shooting && !reloading && bulletsLeft > 0)
            //{
            //    _muzzleFlash.SendEvent("OnPlay");
            //}
        }
    }
    private void Shoot()
    {
        readyToShoot = false;


        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        spawnMuzzle();
        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            Target target = rayHit.transform.GetComponent<Target>();

            if (rayHit.collider.CompareTag("Enemy"))
                target.TakeDamage(damage);
        }

        transform.GetComponent<AdvancedWeaponRecoil>().Fire();

        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

    }

    void spawnMuzzle()
    {
        VisualEffect newMuzzleEffect = Instantiate(_brustPrefab,  _muzzleFlash.transform.position, _muzzleFlash.transform.rotation,transform);
        newMuzzleEffect.Play();
        Destroy(newMuzzleEffect.gameObject, spread);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        GunAnim.SetTrigger("reload");

        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
