using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float hitPerHealth = -25f;
    [SerializeField] private float timeToDestruct = 4f;
    [SerializeField] private int scorePerDestory = 100;
    [SerializeField] private int scorePerHit = 15;
    [SerializeField] private GameObject fxExplotion;
    [SerializeField] private GameObject fxHit;

    private ScoreBoard scoreboard;
    private State state;

    Rigidbody rb;
    GameObject parentGameObject;

    private enum State
    {
        LIVE,
        DEAD
    }

    private void Start()
    {
        scoreboard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");

        AddRigidbody();
    }

    private void AddRigidbody()
    {
        rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        ProcessEnemyDead();
    }

    private void ProcessHit()
    {
        health += hitPerHealth;
        scoreboard.IncreaseScore(scorePerHit);
        MakeEffect(fxHit);
    }

    private void ProcessEnemyDead()
    {
        if (health < 1 && state == State.LIVE)
        {
            state = State.DEAD;
            StartEnemyDeadSequence();
        }
    }

    private void StartEnemyDeadSequence()
    {
        MakeEffect(fxExplotion);
        GetComponent<MeshRenderer>().enabled = false;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(gameObject, timeToDestruct);
        scoreboard.IncreaseScore(scorePerDestory);
    }

    private void MakeEffect(GameObject gameobject)
    {
        GameObject fx = Instantiate(gameobject, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
    }
}