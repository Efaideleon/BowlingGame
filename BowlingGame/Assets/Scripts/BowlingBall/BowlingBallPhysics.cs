using UnityEngine;

public class BowlingBallPhysics : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier = 20f;
    [SerializeField] private float _spinMultiplier = 10f;
    [SerializeField] private float _maxChargeTime = 2;

    private Rigidbody _rb;
    private float _chargeStartTime;
    private bool _isCharging = false;
    public Vector3 _throwForce;
    private Vector3 _spinTorque;

    public bool IsSettled => _rb.linearVelocity.magnitude < 0.4f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void StartCharging()
    {
        _isCharging = true;
        _chargeStartTime = Time.time;
    }

    public void StopCharging()
    {
        if (_isCharging)
        {
            (_throwForce, _spinTorque) = CalculateReleaseForces();
            _isCharging = false;
        }
    }

    public float ChargePercentage => Mathf.Clamp01(GetCurrentCharge() / _maxChargeTime);

    public void OnHold()
    {
        _rb.isKinematic = true;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    } 

    public void Throw()
    {
        _rb.isKinematic = false;
        ApplyForcesToBall(_throwForce, _spinTorque);
    }

    private float GetCurrentCharge()
    {
        if (!_isCharging) return 0f;
        return Mathf.Min(Time.time - _chargeStartTime, _maxChargeTime);
    }

    private (Vector3, Vector3) CalculateReleaseForces()
    {
        float chargeDuration = GetCurrentCharge();
        float power = Mathf.Clamp01(chargeDuration / _maxChargeTime);
        Vector3 throwForce = _forceMultiplier * power * Vector3.forward;

        float spin = Input.GetAxis("Horizontal");
        Vector3 spinTorque = spin * _spinMultiplier * Vector3.up;

        return (throwForce, spinTorque);
    }

    private void ApplyForcesToBall(Vector3 throwForce, Vector3 spinTorque)
    {
        _rb.AddForce(throwForce, ForceMode.Impulse);
        _rb.AddTorque(spinTorque);
    }
}
