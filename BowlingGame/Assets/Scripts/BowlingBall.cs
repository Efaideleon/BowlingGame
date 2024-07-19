using UnityEngine;

public class BowlingBall : Chargeable 
{
    [SerializeField] float forceMultiplier = 50f;
    [SerializeField] float spinMultiplier = 10f;
    [SerializeField] float maxChargeTime = 1.5f;
    private Rigidbody rb;
    private float chargeStartTime;
    private bool isCharging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
    }

    private bool IsFullyCharged()
    {
        return Time.time - chargeStartTime >= maxChargeTime;
    }

    private (Vector3, Vector3) CalculateReleaseForces()
    {
        float chargeDuration = Time.time - chargeStartTime;
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

    public override void ReleaseCharge()
    {
        if (isCharging || IsFullyCharged())
        {
            (Vector3 throwForce, Vector3 spinTorque) = CalculateReleaseForces();
            ApplyForcesToBall(throwForce, spinTorque);
            isCharging = false;
        }
    }
}
