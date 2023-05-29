using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarks : MonoBehaviour
{
    public TrailRenderer[] bloodMarks;
    bool isGround =false, bloodMarksFlag;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            startEmitter();
        }
        else stopEmitter();
    }
   
    void startEmitter()
    {
        if (bloodMarksFlag) return;
        foreach (TrailRenderer T in bloodMarks)
        {
            T.emitting = true;
        }
        bloodMarksFlag = true;
    }

    void stopEmitter()
    {
        if (!bloodMarksFlag) return;
        foreach (TrailRenderer T in bloodMarks)
        {
            T.emitting = false;
        }
        bloodMarksFlag = false;
    }
}
