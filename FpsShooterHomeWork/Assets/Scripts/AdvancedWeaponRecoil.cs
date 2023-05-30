using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedWeaponRecoil : MonoBehaviour
{
    [Header("Reference Points:")]
    public Transform recoilPostion;
    public Transform rotationPostion;

    [Space(10)]
    [Header("Speed Settings:")]
    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;

    [Space(10)]
    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 18f;
    [Space(10)]

    [Header("Amount Settings:")]
    public Vector3 RecoilRotation = new Vector3(10, 5, 7);
    public Vector3 RecoilKickBack = new Vector3(0.015f, 0f, -0.2f);

    [Space(10)]
    public Vector3 RecoilRotationAim = new Vector3(10, 4, 6);
    public Vector3 RecoilKickBackAim = new Vector3(0.015f, 0f, -0.2f);
    [Space(10)]

    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 Rot;
    [Header("State:")]
    public bool aiming;

    [Header("WeaponSway")]
    [Range(0, 1)]
    public float amount;
    [Range(0, 1)]
    public float MaxAmount;
    [Range(0, 10)]
    public float smootAmount;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = recoilPostion.localPosition;
    }

    // Update is called once per frame
    void Update()
    {      
        if (Input.GetButton("Fire2"))
        {
            aiming = true;
        }
        else
        {
            aiming = false;
        }
    }

    private void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPostion.localPosition = Vector3.Slerp(recoilPostion.localPosition, positionalRecoil, positionalReturnSpeed * Time.deltaTime);
        Rot = Vector3.Slerp(Rot, rotationalRecoil, rotationalReturnSpeed * Time.fixedDeltaTime);
        rotationPostion.localRotation = Quaternion.Euler(Rot);

        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -MaxAmount, MaxAmount);
        movementY = Mathf.Clamp(movementY, -MaxAmount, MaxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        recoilPostion.localPosition = Vector3.Lerp(recoilPostion.localPosition, finalPosition + initialPosition, Time.deltaTime * smootAmount);


    }


    public void Fire()
    {
        if (aiming)
        {
            rotationalRecoil += new Vector3(-RecoilRotationAim.x, Random.Range(-RecoilRotationAim.y, RecoilRotationAim.y), Random.Range(-RecoilRotationAim.z, RecoilRotationAim.z));
            positionalRecoil += new Vector3(Random.Range(-RecoilKickBackAim.x, RecoilKickBackAim.x), Random.Range(-RecoilKickBackAim.y, RecoilKickBackAim.y), RecoilKickBackAim.z);
        }
        else
        {
            rotationalRecoil += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
            positionalRecoil += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
        }
    }
}
