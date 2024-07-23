using UnityEditor.Callbacks;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool isFallen = false;
    private Vector3 initialPosition; 
    private new Rigidbody rigidbody;
    void Start()
    {
        initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isFallen && CheckIfFallen()) 
        {
            isFallen = true;
            GameManager.Instance.PinFallen();
        }
    }

    private bool CheckIfFallen()
    {
        return transform.up.y < 0.5f;
    }

    public void ResetPin()
    {
        isFallen = false;
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(initialPosition, Quaternion.identity);
    }
}
