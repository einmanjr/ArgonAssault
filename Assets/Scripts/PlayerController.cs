using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float ControlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 4.5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;
    [Tooltip("lasers")] [SerializeField] GameObject[] lasers;

    [Header("Screen Position-Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Control Throw-Based")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -15f;


    float xThrow, yThrow;
    bool ControlsEnabled = true;


    void Update()
    {
        if (ControlsEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void OnPlayerDeath()                // called by string reference
    {
        ControlsEnabled = false;
    }
     
    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    { 
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * ControlSpeed * Time.deltaTime;
        float yOffset = yThrow * ControlSpeed * Time.deltaTime;


        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if(CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateLasers();
        }
        else
        {
            DeactivateLasers();
        }
    }

    private void ActivateLasers()
    {
        foreach(GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
    }

    private void DeactivateLasers()
    {
        foreach(GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }
}
