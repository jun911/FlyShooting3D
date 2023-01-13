using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float velocity = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float xValue = Input.GetAxis("Vertical");
        float yValue = Input.GetAxis("Horizontal");
        Vector3 vec = new Vector3(xValue, yValue, 0);

        transform.Rotate(vec, rotateSpeed, Space.World);
        //transform.TransformDirection(vec.normalized * velocity * Time.fixedDeltaTime);
        transform.position = transform.position + transform.forward * velocity * Time.fixedDeltaTime;
    }
}