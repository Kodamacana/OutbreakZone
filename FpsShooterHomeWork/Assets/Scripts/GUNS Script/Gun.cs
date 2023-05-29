using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX; 

public class Gun : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] bool AR = false;
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
    public GameObject Crosshair;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public VisualEffect _muzzleFlash;
    public VisualEffect _brustPrefab;
    public GameObject bulletHoleGraphic;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    [Header("Sounds")]
    [SerializeField] public AudioSource GunSoundSource;
    [SerializeField] public AudioClip Reload1;
    [SerializeField] public AudioClip Reload2;
    [SerializeField] public AudioClip Fire;
    [SerializeField] public AudioClip Equip;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        
    }

    public void PlayReload1()
    {
        InstantiateAudio(Reload1);
        playSound();
    }
    public void PlayReload2()
    {
        InstantiateAudio(Reload2);
        playSound();
    }
    public void PlayFire()
    {
        InstantiateAudio(Fire);
        playSound();
    }
    public void PlayEquip()
    {
        InstantiateAudio(Equip);
        playSound();
    }

    void InstantiateAudio(AudioClip clip)
    {
        GunSoundSource.clip = clip;
    }

    void playSound()
    {
        if (GunSoundSource.isPlaying)
            GunSoundSource.Stop();
            GunSoundSource.Play();
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
            if (AR)
            {
                bool ARFire = false;
                float mouthSmile_L = skinnedMeshRenderer.GetBlendShapeWeight(skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("mouthSmile_L"));
                float mouthSmile_R = skinnedMeshRenderer.GetBlendShapeWeight(skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("mouthSmile_R"));
                if (mouthSmile_L >= 40 && mouthSmile_R >= 40) ARFire = true;

                canShoot = ARFire && readyToShoot && !reloading && bulletsLeft > 0;
            }

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            {
                Reload();
            }

            if (canShoot)
            {
                bulletsShot = bulletsPerTap;
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        PlayFire();
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        spawnMuzzle();

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            //bullet hole
            if (!rayHit.transform.name.Contains("Zombi"))
            {
                GameObject obj = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                obj.transform.position += obj.transform.forward / 1000;
            }            
            //

            Target target = rayHit.transform.GetComponent<Target>();

            if (rayHit.collider.CompareTag("Enemy"))
                target.TakeDamage(damage);
        }

        transform.GetComponent<AdvancedWeaponRecoil>().Fire();

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
