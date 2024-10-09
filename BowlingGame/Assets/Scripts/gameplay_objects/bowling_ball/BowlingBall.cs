using UnityEngine;

namespace bowling_ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BowlingBall : MonoBehaviour
    {
        [SerializeField] private float _forceMultiplier = 40f;
        [SerializeField] private float _spinMultiplier = 10f;

        [SerializeField] Rigidbody _rb;

        public bool IsSettled => _rb.linearVelocity.magnitude < 0.4f;

        public bool IsRolling => _rb.linearVelocity.magnitude >= 0.4f; // Move magic number

        public void OnHold()
        {
            _rb.isKinematic = false;
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.isKinematic = true;
        }

        public void Throw(float power)
        {
            _rb.isKinematic = false;
            var (_throwForce, _spinTorque) = CalculateReleaseForces(power);
            ApplyForcesToBall(_throwForce, _spinTorque);
        }

        private (Vector3, Vector3) CalculateReleaseForces(float power)
        {
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
}