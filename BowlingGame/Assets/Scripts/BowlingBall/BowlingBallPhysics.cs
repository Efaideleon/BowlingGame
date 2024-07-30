using UnityEngine;

public class BowlingBallPhysics : MonoBehaviour
{
    [SerializeField] private float forceMultiplier = 20f;
    [SerializeField] private float spinMultiplier = 10f;
    [SerializeField] private float maxChargeTime = 2;

    private Rigidbody rb;
    private float chargeStartTime;
    private bool isCharging = false;
    private Vector3 throwForce;
    private Vector3 spinTorque;

    public bool IsSettled => rb.linearVelocity.magnitude < 0.4f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
    }

    public void StopCharging()
    {
        if (isCharging)
        {
            (throwForce, spinTorque) = CalculateReleaseForces();
            isCharging = false;
        }
    }

    public float ChargePercentage => Mathf.Clamp01(GetCurrentCharge() / maxChargeTime);

    public void OnHold()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    } 

    public void Throw()
    {
        rb.isKinematic = false;
        ApplyForcesToBall(throwForce, spinTorque);
    }

    private float GetCurrentCharge()
    {
        if (!isCharging) return 0f;
        return Mathf.Min(Time.time - chargeStartTime, maxChargeTime);
    }

    private (Vector3, Vector3) CalculateReleaseForces()
    {
        float chargeDuration = GetCurrentCharge();
        float power = Mathf.Clamp01(chargeDuration / maxChargeTime);
        Vector3 throwForce = forceMultiplier * power * Vector3.forward;

        float spin = Input.GetAxis("Horizontal");
        Vector3 spinTorque = spin * spinMultiplier * Vector3.up;

        return (throwForce, spinTorque);
    }

    private void ApplyForcesToBall(Vector3 throwForce, Vector3 spinTorque)
    {
        rb.AddForce(throwForce, ForceMode.Impulse);
        rb.AddTorque(spinTorque);
    }
}
