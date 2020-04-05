using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is the only script that loads scenes

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")]          [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;
    

    void OnTriggerEnter(Collider other)
    {
        deathFX.SetActive(true);
        StartDeathSequence();
        Invoke("ReloadScene", levelLoadDelay);      // string reference 1
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene()              // string reference 1
    {
        SceneManager.LoadScene(1);
    }
}
