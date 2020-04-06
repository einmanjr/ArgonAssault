using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Easter Egg")] [SerializeField] GameObject deathFX;
    [Tooltip("Parent to all")] [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 25;
    [SerializeField] int hits = 10;

    ScoreBoard scoreBoard;

        void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        scoreBoard.scoreHit(scorePerHit);
        hits -= 1;        //TODO consider hit FX
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
