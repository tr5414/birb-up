using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool debug = false;

    public float shakeAmount = 0.5f;
    public float shakeLength = 1.0f;

    public bool smoothing;
    public float smoothAmount = 5f;

    bool isRunning = false;


    float shakePercent;
    float startAmount;
    float startDuration;


    // Start is called before the first frame update
    void Start()
    {
        if (debug) Shake();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void ShakeCamera()
    {
        startAmount = shakeAmount;
        startDuration = shakeLength;

        if (!isRunning) StartCoroutine(Shake());
    }


    public void ShakeCamera(float amount, float duration)
    {
        shakeAmount += amount;
        shakeLength += duration;

        ShakeCamera();
    }

    IEnumerator Shake()
    {
        isRunning = true;

        while (shakeLength > 0.01f)
        {
            Vector3 rotationAmt = Random.insideUnitSphere * shakeLength;
            rotationAmt.z = 0;

            shakePercent = shakeLength / startDuration;

            shakeAmount = startAmount * shakePercent;
            shakeLength = Mathf.Lerp(shakeLength, 0, Time.deltaTime);


            if (smoothing)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmt), Time.deltaTime * smoothAmount);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(rotationAmt);
            }

            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isRunning = false;
    }

}
