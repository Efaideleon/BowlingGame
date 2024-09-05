using UnityEngine;

public class Pin : MonoBehaviour {
    private Vector3 _initialPosition;
    private Rigidbody _rb;
    public bool IsFallen => transform.up.y < 0.5f;
    public bool IsSettled => _rb.linearVelocity.magnitude < 1;

    void Start() {
        _initialPosition = transform.position;
        _rb = GetComponent<Rigidbody>();
    }

    public void ResetPin() {
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(_initialPosition, Quaternion.identity);
        gameObject.SetActive(true);
    }
}
