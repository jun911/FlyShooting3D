using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float hitPerHealth = -25f;
    [SerializeField] float timeToDestruct = 4f;
    [SerializeField] int scorePerDestory = 100;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] GameObject fxExplotion;
    [SerializeField] AudioClip sfxExplotion;
    [SerializeField] Transform parent;

    ScoreBoard scoreboard;
    AudioSource audioSource;
    State state;

    enum State
    {
        LIVE,
        DEAD
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scoreboard = GameManager.instance.scoreBoard;
    }

    private void OnParticleCollision(GameObject other)
    {
        health += hitPerHealth;
        scoreboard.IncreaseScore(scorePerHit);

        if (health < 1 && state == State.LIVE)
        {
            state = State.DEAD;
            StartEnemyDeadSequence();
        }
    }

    private void StartEnemyDeadSequence()
    {
        MakeEffect(fxExplotion);
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(sfxExplotion);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, timeToDestruct);
        scoreboard.IncreaseScore(scorePerDestory);
    }

    private void MakeEffect(GameObject gameobject)
    {
        GameObject vfx = Instantiate(gameobject, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
    }
}