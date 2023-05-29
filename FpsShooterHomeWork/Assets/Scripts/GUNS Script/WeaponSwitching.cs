using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    [SerializeField] GameObject Crosshair;

    // Start is called before the first frame update
    void Start()
    {
        SelectedWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelecytedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
            selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;

        }

        if (previousSelecytedWeapon != selectedWeapon)
        {
            SelectedWeapon();
        }
    }
    void SelectedWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            Gun gunComponent = weapon.GetComponentInChildren<Gun>();
            if (gunComponent != null)
            {
                bool isSelectedWeapon = (i == selectedWeapon);

                weapon.gameObject.SetActive(isSelectedWeapon);
                gunComponent.Crosshair.SetActive(isSelectedWeapon);

                AudioSource audioSource = weapon.GetComponentInChildren<AudioSource>();
                if (audioSource != null)
                    audioSource.clip = null;

                if (!isSelectedWeapon)
                    gunComponent.Crosshair.SetActive(false);

                if(gunComponent._muzzleFlash != null) gunComponent._muzzleFlash.gameObject.SetActive(!isSelectedWeapon);

                i++;
            }
        }
    }

}
