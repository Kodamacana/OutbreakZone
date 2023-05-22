using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowShoot : MonoBehaviour
{
    [SerializeField] float pullSpeed;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject arrow;
    [SerializeField] int numberOfArrows = 10;
    [SerializeField] GameObject bow;
    [SerializeField] Vector3 ArrowPosition;
    Quaternion ArrowRotation;
    bool arrowSlotted = false;
    float pullAmounth = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        ArrowRotation = new Quaternion(-0.485117644f, 0.421706289f, -0.578141749f, 0.502570271f);
        SpawnArrow();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLogic();   
    }

    void SpawnArrow()
    {
        if(numberOfArrows > 0)
        {
            arrowSlotted = true;
            arrow = Instantiate(arrowPrefab, transform) as GameObject;
            arrow.transform.parent = transform;

            arrow.transform.localPosition = ArrowPosition;
            arrow.transform.localRotation = ArrowRotation;
        }
    }

    void ShootLogic()
    {
        Debug.Log("trs: "+arrow.transform.localPosition);
        if (numberOfArrows > 0)
        {
            if (pullAmounth < 100) pullAmounth = 100;

            SkinnedMeshRenderer _bowSkin = bow.transform.GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer _arrowSkin = arrow.transform.GetComponent<SkinnedMeshRenderer>();

            Rigidbody _arrowRigidB = arrow.transform.GetComponent<Rigidbody>();

            BowProjectileAddForce _arrowProjectile = arrow.transform.GetComponent<BowProjectileAddForce>();
            if (Input.GetMouseButton(0))
            {
                pullAmounth += Time.deltaTime * pullSpeed;
            }
            if (Input.GetMouseButtonUp(0))
            {
               
                arrowSlotted = false;
                _arrowRigidB.isKinematic = false;
                arrow.transform.parent = null;
                _arrowProjectile.shootForce = _arrowProjectile.shootForce * ((pullAmounth / 100) + .05f);
                numberOfArrows -= 1;
                pullAmounth = 0;

                _arrowProjectile.enabled = true;
                Invoke("destroyObj", 1f);

            }
            //_bowSkin.SetBlendShapeWeight(0, pullAmounth);
            //_arrowSkin.SetBlendShapeWeight(0, pullAmounth);
            
            if (Input.GetMouseButtonDown(0) && arrowSlotted == false)
                SpawnArrow();
        }

        void destroyObj()
        {
            Destroy(arrow.gameObject);
        }
    }
}
