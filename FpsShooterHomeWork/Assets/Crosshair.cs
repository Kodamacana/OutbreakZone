using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public float movementThreshold = 25.0f;
    float detectionInterval = 0.2f;
    float smoothTime = 0.1f;

    float lastDetectionTime = 0;

    Vector3 lastPosition;
    Vector3 targetPosition;
    Vector3 velocity;

    bool CalibrationMode = false;


    void Start()
    {
        CalibrationMode = true;
        float repeatRate = 0.1f; // Saniye başı çağırma hızı
        InvokeRepeating("UpdateWithInterval", 0f, repeatRate);
        lastPosition = transform.localPosition;
        targetPosition = lastPosition;
        velocity = Vector3.zero;
    }
    [SerializeField] GameObject ArEyes_L;
    [SerializeField] GameObject ArEyes_R;

    public float top = 0;
    public float bot = 0;
    public float right = 0;
    public float left = 0;
    public float LeftBot = 0;
    public float RightBot = 0;
    public float RightTop = 0;
    public float LeftTop = 0;
    public float RightLeftHalfTop = 0;
    public float RightLeftHalfBot = 0;
    public float LeftTopHalf = 0;
    public float LeftBotHalf = 0;
    public float RightBotHalf = 0;
    public float RightTopHalf = 0;
    public float MiddleY = 0;
    public float MiddleX = 0;
    public float RightTopBot = 0;
    public float LeftTopBot = 0;

    public GameObject[] CalibrationPos;

    int index = 0;

    private void UpdateWithInterval()
    {
        if (!CalibrationMode)
        {
            CalculateCrosshairPosition();
        }
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnButtonClick();
        }
    }


    public void OnButtonClick()
    {
        if (index == 0)
        {
            top = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false);
            index++;
            CalibrationPos[index].SetActive(true);
        }
        else if (index == 1)
        {
            bot = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 2)
        {
            right = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 3)
        {
            left = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 4)
        {
            LeftBot = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 5)
        {
            LeftBotHalf = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 6)
        {
            LeftTop = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 7)
        {
            LeftTopHalf = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 8)
        {
            RightBot = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 9)
        {
            RightBotHalf = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 10)
        {
            RightTopHalf = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 11)
        {
            MiddleY = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 12)
        {
            MiddleX = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 13)
        {
            LeftTopBot = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 14)
        {
            RightTopBot = ArEyes_R.transform.localRotation.x;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 15)
        {
            RightTop = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 16)
        {
            RightLeftHalfBot = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++; CalibrationPos[index].SetActive(true);
        }
        else if (index == 17)
        {
            RightLeftHalfTop = ArEyes_R.transform.localRotation.y;
            CalibrationPos[index].SetActive(false); index++;
        }
        else if (index >= 18)
        {
            CalibrationMode = false;

        }
    }

    void CalculateCrosshairPosition()
    {
        //if (Time.time - lastDetectionTime >= detectionInterval)
        //{
        float NegativeOranX = 960 / left;
        var x = ArEyes_R.transform.localRotation.y * NegativeOranX;

        float NegativeOranY = 536 / bot;
        var y = ArEyes_R.transform.localRotation.x * NegativeOranY;


        float OranX = 960 / right;
        var w = ArEyes_R.transform.localRotation.y * OranX;

        float OranY = 536 / top;
        var k = ArEyes_R.transform.localRotation.x * OranY;

        //orta
        if ((ArEyes_R.transform.localRotation.y <= RightTopBot && ArEyes_R.transform.localRotation.y >= LeftTopBot) && (ArEyes_R.transform.localRotation.x >= RightLeftHalfBot && ArEyes_R.transform.localRotation.x <= RightLeftHalfTop))
        {
            //orta sol
            if (ArEyes_R.transform.localRotation.y <= MiddleX)
            {
                //orta üst
                if (ArEyes_R.transform.localRotation.x <= RightLeftHalfTop && ArEyes_R.transform.localRotation.x >= MiddleY)
                {
                    NegativeOranX = 480 / LeftTop;
                    x = ArEyes_R.transform.localRotation.y * NegativeOranX;

                    NegativeOranY = 255 / LeftTopHalf;
                    y = ArEyes_R.transform.localRotation.x * NegativeOranY;
                    x = Mathf.Clamp(x, -960, 960);
                    y = Mathf.Clamp(y, -536, 536);
                    if (Mathf.Abs(x - transform.localPosition.x) > movementThreshold || Mathf.Abs(y - transform.localPosition.y) > movementThreshold || Time.time - lastDetectionTime >= detectionInterval)
                    {

                        targetPosition = new Vector3(-x, y, transform.localPosition.z);
                        lastDetectionTime = Time.time;
                    }
                    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 100f);
                    lastPosition = transform.localPosition;
                }
                //orta alt
                else if (ArEyes_R.transform.localRotation.x >= RightLeftHalfBot && ArEyes_R.transform.localRotation.x <= MiddleY)
                {
                    NegativeOranX = 480 / LeftTop;
                    x = ArEyes_R.transform.localRotation.y * NegativeOranX;

                    NegativeOranY = 255 / LeftTopHalf;
                    y = ArEyes_R.transform.localRotation.x * NegativeOranY;
                    x = Mathf.Clamp(x, -960, 960);
                    y = Mathf.Clamp(y, -536, 536);
                    if (Mathf.Abs(x - transform.localPosition.x) > movementThreshold || Mathf.Abs(y - transform.localPosition.y) > movementThreshold || Time.time - lastDetectionTime >= detectionInterval)
                    {

                        targetPosition = new Vector3(-x, -y, transform.localPosition.z);
                        lastDetectionTime = Time.time;
                    }
                    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 100f);
                    lastPosition = transform.localPosition;
                }

            }
            //orta sağ
            else
            {
                //orta üst
                if (ArEyes_R.transform.localRotation.x <= RightLeftHalfTop && ArEyes_R.transform.localRotation.x >= MiddleY)
                {
                    NegativeOranX = 480 / LeftTop;
                    x = ArEyes_R.transform.localRotation.y * NegativeOranX;

                    NegativeOranY = 255 / LeftTopHalf;
                    y = ArEyes_R.transform.localRotation.x * NegativeOranY;
                    x = Mathf.Clamp(x, -960, 960);
                    y = Mathf.Clamp(y, -536, 536);
                    if (Mathf.Abs(x - transform.localPosition.x) > movementThreshold || Mathf.Abs(y - transform.localPosition.y) > movementThreshold || Time.time - lastDetectionTime >= detectionInterval)
                    {

                        targetPosition = new Vector3(x, y, transform.localPosition.z);
                        lastDetectionTime = Time.time;
                    }
                    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 100f);
                    lastPosition = transform.localPosition;
                }
                //orta alt
                else if (ArEyes_R.transform.localRotation.x >= RightLeftHalfBot && ArEyes_R.transform.localRotation.x <= MiddleY)
                {
                    NegativeOranX = 480 / LeftTop;
                    x = ArEyes_R.transform.localRotation.y * NegativeOranX;

                    NegativeOranY = 255 / LeftTopHalf;
                    y = ArEyes_R.transform.localRotation.x * NegativeOranY;
                    x = Mathf.Clamp(x, -960, 960);
                    y = Mathf.Clamp(y, -536, 536);
                    if (Mathf.Abs(x - transform.localPosition.x) > movementThreshold || Mathf.Abs(y - transform.localPosition.y) > movementThreshold || Time.time - lastDetectionTime >= detectionInterval)
                    {

                        targetPosition = new Vector3(x, y, transform.localPosition.z);
                        lastDetectionTime = Time.time;
                    }
                    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 100f);
                    lastPosition = transform.localPosition;
                }
            }


        }
        //Sol
        else if (ArEyes_R.transform.localRotation.y <= MiddleX)
        {
            Debug.Log("sol");


            //alt
            if (ArEyes_R.transform.localRotation.x >= MiddleY)
            {
                Debug.Log("solAlt");
                NegativeOranX = 960 / LeftBot;
                x = ArEyes_R.transform.localRotation.y * NegativeOranX;

                NegativeOranY = 536 / LeftBotHalf;
                y = ArEyes_R.transform.localRotation.x * NegativeOranY;
                x = Mathf.Clamp(x, -960, 960);
                y = Mathf.Clamp(y, -536, 536);
                if (Mathf.Abs(x - transform.localPosition.x) > movementThreshold || Mathf.Abs(y - transform.localPosition.y) > movementThreshold || Time.time - lastDetectionTime >= detectionInterval)
                {

                    targetPosition = new Vector3(-x, -y, transform.localPosition.z);
                    lastDetectionTime = Time.time;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 100f);
                lastPosition = transform.localPosition;
            }
            //üst
            else
            {
                Debug.Log("solÜst");
                NegativeOranX = 960 / LeftTop;
                x = ArEyes_R.transform.localRotation.y * NegativeOranX;

                NegativeOranY = 536 / LeftTopHalf;
                y = ArEyes_R.transform.localRotation.x * NegativeOranY;
                x = Mathf.Clamp(x, -960, 960);
                y = Mathf.Clamp(y, -536, 536);
                if (Mathf.Abs(x - transform.localPosition.x) > movementThreshold || Mathf.Abs(y - transform.localPosition.y) > movementThreshold || Time.time - lastDetectionTime >= detectionInterval)
                {

                    targetPosition = new Vector3(-x, y, transform.localPosition.z);
                    lastDetectionTime = Time.time;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 100f);
                lastPosition = transform.localPosition;
            }

        }
        //sağ
        else
        {
            Debug.Log("Sağ");
            //alt
            if (ArEyes_R.transform.localRotation.x >= MiddleY)
            {
                Debug.Log("SağAlt");
                NegativeOranX = 960 / RightBot;
                x = ArEyes_R.transform.localRotation.y * NegativeOranX;

                NegativeOranY = 536 / RightBotHalf;
                y = ArEyes_R.transform.localRotation.x * NegativeOranY;
                x = Mathf.Clamp(x, -960, 960);
                y = Mathf.Clamp(y, -536, 536);
                if (Mathf.Abs(x - transform.localPosition.x) > movementThreshold || Mathf.Abs(y - transform.localPosition.y) > movementThreshold || Time.time - lastDetectionTime >= detectionInterval)
                {

                    targetPosition = new Vector3(x, -y, transform.localPosition.z);
                    lastDetectionTime = Time.time;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 100f);
                lastPosition = transform.localPosition;
            }
            //üst
            else
            {
                Debug.Log("SağÜst");
                NegativeOranX = 960 / RightTop;
                x = ArEyes_R.transform.localRotation.y * NegativeOranX;

                NegativeOranY = 536 / RightTopHalf;
                y = ArEyes_R.transform.localRotation.x * NegativeOranY;
                x = Mathf.Clamp(x, -960, 960);
                y = Mathf.Clamp(y, -536, 536);

                if (Mathf.Abs(x - transform.localPosition.x) > movementThreshold || Mathf.Abs(y - transform.localPosition.y) > movementThreshold || Time.time - lastDetectionTime >= detectionInterval)
                {
                    targetPosition = new Vector3(x, y, transform.localPosition.z);
                    lastDetectionTime = Time.time;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 100f);
                lastPosition = transform.localPosition;
            }


        }

        //}
    }
    //public void ChangePos()
    //{
    //    transform.localPosition = new Vector3( horizontal.value, vertical.value);
    //}
}
