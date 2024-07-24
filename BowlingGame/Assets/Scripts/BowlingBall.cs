using UnityEditor.Callbacks;
using UnityEngine;

public class BowlingBall : Chargeable 
{
    [SerializeField] float forceMultiplier = 20f;
    [SerializeField] float spinMultiplier = 10f;
    [SerializeField] float maxChargeTime = 2;
    [SerializeField] GameManager gameManager;
    private new Rigidbody rigidbody;
    private float chargeStartTime;
    private bool isCharging = false;
    private Vector3 initialPosition;
    private Vector3 throwForce;
    private Vector3 spinTorque;
    void Start()
    {
        initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void StartCharging()
    {
        isCharging = true;
        chargeStartTime = Time.time;
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
        rigidbody.AddForce(throwForce, ForceMode.Impulse);
        rigidbody.AddTorque(spinTorque);
    }

    public override void ReleaseCharge()
    {
        if (isCharging)
        {
            (throwForce, spinTorque) = CalculateReleaseForces();
            isCharging = false;
        }
    }

    public void Throw()
    {
        rigidbody.isKinematic = false;
        ApplyForcesToBall(throwForce, spinTorque);
        gameManager.CheckPinsAfterThrow(); 
    }

    public override float GetChargePercentage()
    {
        return Mathf.Clamp01(GetCurrentCharge() / maxChargeTime);
    }

    public void ResetBall()
    {
        transform.SetPositionAndRotation(initialPosition, Quaternion.identity);
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public void OnHold(Transform parent)
    {
        rigidbody.isKinematic = true;
        transform.parent = parent;
        transform.SetLocalPositionAndRotation(new Vector3(0, 0.95f, 0.37f), Quaternion.identity);
    } 
}
