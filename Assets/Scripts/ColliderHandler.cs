using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{this.name}--collied with--" + collision.gameObject.name);

        StartPlayerCrashSequence();
    }

    private void StartPlayerCrashSequence()
    {
        // health check??

        player.StartPlayerCrashSequence();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name}**triggered with**" + other.name);
    }
}