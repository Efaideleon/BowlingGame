using UnityEngine;

public class BowlingBall : MonoBehaviour
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

    void Update()
    {
        // Player presses the space bar.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCharging();
        }
        // Player lets go of the space bar or power is fully charged.
        if (Input.GetKeyUp(KeyCode.Space) || IsFullyCharged())
        {
            if (isCharging)
            {
                ReleaseBall();
            }
        }
    }

    private void StartCharging()
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

        Vector3 throwForce = Vector3.forward * power * forceMultiplier;

        float spin = Input.GetAxis("Horizontal");
        Vector3 spinTorque = Vector3.up * spin * spinMultiplier;

        return (throwForce, spinTorque);
    }

    private void ApplyForcesToBall(Vector3 throwForce, Vector3 spinTorque)
    {
        rb.AddForce(throwForce, ForceMode.Impulse);
        rb.AddTorque(spinTorque);
    }

    private void ReleaseBall()
    {
        (Vector3 throwForce, Vector3 spinTorque) = CalculateReleaseForces();
        ApplyForcesToBall(throwForce, spinTorque);
        isCharging = false;
    }
}
