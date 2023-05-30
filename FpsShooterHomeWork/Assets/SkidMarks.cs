using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarks : MonoBehaviour
{
    public TrailRenderer bloodMarks1;
    public TrailRenderer bloodMarks2;
    public TrailRenderer bloodMarks3;
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
       
        bloodMarks1.emitting = true;
        bloodMarks2.emitting = true;
        bloodMarks3.emitting = true;
        
        bloodMarksFlag = true;
    }

    void stopEmitter()
    {
        if (!bloodMarksFlag) return;

        bloodMarks1.emitting = false;
        bloodMarks2.emitting = false;
        bloodMarks3.emitting = false;

        bloodMarksFlag = false;
    }
}
