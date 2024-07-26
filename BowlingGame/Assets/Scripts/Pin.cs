using UnityEngine;

public class Pin : MonoBehaviour
{
    private Vector3 initialPosition; 
    private new Rigidbody rigidbody;
    public bool IsFallen => transform.up.y < 0.5f;
    public bool IsSettled => rigidbody.linearVelocity.magnitude < 1;

    void Start()
    {
        initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetPin()
    {
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(initialPosition, Quaternion.identity);
        gameObject.SetActive(true);
    }
}
