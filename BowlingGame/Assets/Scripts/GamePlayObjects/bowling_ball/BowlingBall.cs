using UnityEngine;

namespace bowling_ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BowlingBall : MonoBehaviour
    {
        [SerializeField] private float _forceMultiplier = 40f;
        [SerializeField] private float _spinMultiplier = 10f;
        [SerializeField] private float _lateralFriction = 0.5f;
        [SerializeField] private float _rayCastDistance = 0.1f;
        [SerializeField] private float _settledLinearVelocity = 0.4f;
        [SerializeField] private Rigidbody _rb;

        private int _laneLayerMask;
        private Vector3 _throwDirection;

        /// <summary>
        /// Check if that ball is currently moving.
        /// </summary>
        public bool IsSettled => _rb.linearVelocity.magnitude < _settledLinearVelocity ? true : false;

        void Awake()
        {
            _laneLayerMask = 1 << LayerMask.NameToLayer("Lane");
        }

        void FixedUpdate()
        {
            ApplyMotionForces();
        }

        /// <summary>
        /// Stops all physics motion on the ball.
        /// </summary>
        public void OnHold()
        {
            StopAllBallMotion();
        }

        /// <summary>
        /// Throwing the ball with applied physics.
        /// </summary>
        /// <param name="power">A multiplier to the force applied to the ball at throw</param>
        /// <param name="throwDirection">The direction that ball should rotate.</param>
        public void Throw(float power, Vector3 throwDirection)
        {
            _rb.isKinematic = false;
            _throwDirection = throwDirection;

            var throwForce = CalculateThrowForce(power);
            var spinTorque = CalculateSpinTorque();

            _rb.AddForce(throwForce, ForceMode.Impulse);
            _rb.AddTorque(spinTorque, ForceMode.Impulse);
        }

        private void StopAllBallMotion()
        {
            _rb.isKinematic = false;
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.isKinematic = true;
        }

        private void ApplyMotionForces()
        {
            var groundHit = GetGroundHitInfo();
            if (!groundHit.HasValue) return;

            var hitInfo = groundHit.Value;
            var surfaceFriction = hitInfo.collider.material.dynamicFriction;
            var friction = CalculateFrictionForce(surfaceFriction);
            var lateralForce = CalculateLateralForce(surfaceFriction);

            DebugLogInfo(hitInfo, friction, lateralForce);

            _rb.AddForce(friction, ForceMode.Force);
            _rb.AddForce(lateralForce, ForceMode.Force);
        }

        private RaycastHit? GetGroundHitInfo()
        {
            RaycastHit hitInfo;
            bool hitGround = Physics.Raycast(
                origin: transform.position,
                direction: Vector3.down,
                hitInfo: out hitInfo,
                maxDistance: _rayCastDistance,
                layerMask: _laneLayerMask,
                queryTriggerInteraction: QueryTriggerInteraction.Collide
            );

            return hitGround ? hitInfo : null;
        }

        private Vector3 CalculateFrictionForce(float surfaceFriction)
        {
            return -_rb.linearVelocity.normalized * Physics.gravity.magnitude * surfaceFriction;
        }

        private Vector3 CalculateLateralForce(float surfaceFriction)
        {
            return _rb.angularVelocity.y * _lateralFriction * surfaceFriction * _throwDirection;
        }

        private Vector3 CalculateSpinTorque()
        {
            return _spinMultiplier * Vector3.up;
        }

        private Vector3 CalculateThrowForce(float power)
        {
            return _forceMultiplier * power * Vector3.forward;
        }

        private void DebugLogInfo(RaycastHit hitInfo, Vector3 friction, Vector3 lateralVelocity)
        {
            var surfaceFriction = hitInfo.collider.material.dynamicFriction;

            Debug.Log($"Surface: {hitInfo.collider.gameObject.name}, Friction: {surfaceFriction}");
            Debug.Log($"Friction Force: {friction}");
            Debug.Log($"Lateral Friction: {_lateralFriction * surfaceFriction}");
        }
    }
}