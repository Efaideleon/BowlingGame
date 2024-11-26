using UnityEngine;

namespace bowling_ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BowlingBall : MonoBehaviour
    {
        [Header("Physics Settings")]
        [SerializeField] private float _spinMultiplier = 10f;
        [SerializeField] private float _forceMultiplier = 40f;
        [SerializeField] private float _settledLinearVelocity = 0.4f;
        [SerializeField, Range(0, 1)] private float _lateralFriction = 0.5f;

        [Header("RayCasting Settings")]
        [SerializeField] private float _rayCastDistance = 0.1f;

        private int _laneLayerMask;
        private Vector3 _throwDirection;
        private Rigidbody _rigidbody;

        /// <summary>
        /// Check if that ball is currently moving.
        /// </summary>
        public bool IsSettled => _rigidbody.linearVelocity.magnitude < _settledLinearVelocity;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
            StopBallMotion();
        }

        /// <summary>
        /// Throwing the ball with applied physics.
        /// </summary>
        /// <param name="power">A multiplier to the force applied to the ball at throw</param>
        /// <param name="throwDirection">The direction that ball should rotate.</param>
        public void Throw(float power, Vector3 throwDirection)
        {
            _rigidbody.isKinematic = false;
            this._throwDirection = throwDirection;

            var throwForce = CalculateThrowForce(power);
            var spinTorque = CalculateSpinTorque();

            _rigidbody.AddForce(throwForce, ForceMode.Impulse);
            _rigidbody.AddTorque(spinTorque, ForceMode.Impulse);
        }

        private void StopBallMotion()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
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

            _rigidbody.AddForce(friction, ForceMode.Force);
            _rigidbody.AddForce(lateralForce, ForceMode.Force);
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
            return -_rigidbody.linearVelocity.normalized *
                    Physics.gravity.magnitude *
                    surfaceFriction;
        }

        private Vector3 CalculateLateralForce(float surfaceFriction)
        {
            return _rigidbody.angularVelocity.y *
                   _lateralFriction *
                   surfaceFriction *
                   _throwDirection;
        }

        private Vector3 CalculateSpinTorque()
        {
            return _spinMultiplier * Vector3.up;
        }

        private Vector3 CalculateThrowForce(float power)
        {
            return _forceMultiplier *
                   Vector3.forward *
                   power;
        }

        private void DebugLogInfo(RaycastHit hitInfo,
                                  Vector3 friction, 
                                  Vector3 lateralVelocity)
        {
            var surfaceFriction = hitInfo.collider.material.dynamicFriction;

            Debug.Log($"Surface: {hitInfo.collider.gameObject.name}, Friction: {surfaceFriction}");
            Debug.Log($"Friction Force: {friction}");
            Debug.Log($"Lateral Friction: {_lateralFriction * surfaceFriction}");
        }
    }
}