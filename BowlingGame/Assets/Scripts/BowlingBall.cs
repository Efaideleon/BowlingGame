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
    private Vector3 throwForce;
    private Vector3 spinTorque;
    public bool IsSettled => rigidbody.linearVelocity.magnitude < 0.4f;

    void Awake()
    {
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

    public override void StopCharging()
    {
        if (isCharging)
        {
            (throwForce, spinTorque) = CalculateReleaseForces();
            isCharging = false;
        }
    }

    public override float GetChargePercentage()
    {
        return Mathf.Clamp01(GetCurrentCharge() / maxChargeTime);
    }

    public void OnHold(Transform holdParent)
    {
        rigidbody.isKinematic = true;
        transform.parent = holdParent;
        transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    } 

    public void SwingPosition(Transform swingPosition)
    {
        transform.parent = swingPosition;
        transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    public void Throw()
    {
        rigidbody.isKinematic = false;
        transform.parent = null;
        ApplyForcesToBall(throwForce, spinTorque);
        gameManager.CheckPinsAfterThrow(); 
    }
}
