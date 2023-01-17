using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float delayToDestruct = 3f;

    private void Update()
    {
        Destroy(this.gameObject, delayToDestruct);
    }
}