using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesTracker : MonoBehaviour
{
    [SerializeField] GameObject crosshairTransform;
    [SerializeField] GameObject ArEyes_L;
    [SerializeField] GameObject ArEyes_R;

    public float top = 0;
    public float bot = 0;
    public float right = 0;
    public float left = 0;

    float minMaxValue = 30f;

    int index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnButtonClick();
        }
    }

    public void OnButtonClick()
    {
        if(index == 0)
        {
            top = ArEyes_R.transform.localRotation.x;
            index++;
        }
        else if(index == 1)
        {
            bot = ArEyes_R.transform.localRotation.x;
            index++;
        }
        else if(index == 2)
        {
            right = ArEyes_R.transform.localRotation.y;
            index++;
        }
        else if(index == 3)
        {
            left = ArEyes_R.transform.localRotation.y;
            index++;
        }
    }

    void CalculateCrosshairPosition()
    {
        if (ArEyes_R.transform.localRotation.y <= 0)
        {
            Debug.Log("Eksi");
            float oranX = ArEyes_R.transform.localRotation.y / left;
            var x = 445 * oranX;

            float oranY = ArEyes_R.transform.localRotation.x / bot;
            var y = 250 * oranY;
            crosshairTransform.transform.localPosition = new Vector3(x, -y, crosshairTransform.transform.localPosition.z);
        }
        else
        {
            Debug.Log("Artı");
            float oranX = ArEyes_R.transform.localRotation.y / right;
            var x = 445 * oranX;

            float oranY = ArEyes_R.transform.localRotation.x / top;
            var y = 250 * oranY;
            crosshairTransform.transform.localPosition = new Vector3(x, -y, crosshairTransform.transform.localPosition.z);
        }




    }

    private void FixedUpdate()
    {
        CalculateCrosshairPosition();
    }

}
