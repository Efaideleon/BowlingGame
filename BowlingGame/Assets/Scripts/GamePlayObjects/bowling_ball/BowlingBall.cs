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
        [SerializeField] private Rigidbody _rb;

        private int laneLayerMask;
        public bool IsSettled => _rb.linearVelocity.magnitude < 0.4f;
        public bool IsRolling => _rb.linearVelocity.magnitude >= 0.4f; // Move magic number

        void Awake()
        {
            laneLayerMask = 1 << LayerMask.NameToLayer("Lane");
        }

        void FixedUpdate()
        {
            ApplyMotionForces();
        }

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

            var releaseForces = CalculateReleaseForces(power);
            ApplyThrowForcesToBall(releaseForces.throwForce, releaseForces.spinTorque);
        }

        private void ApplyMotionForces()
        {
            var groundHit = GetGroundHitInfo();
            if (!groundHit.HasValue) return;

            var hitInfo = groundHit.Value;
            var forces = CalculateBallMotionForces(hitInfo);

            DebugLogInfo(hitInfo, forces.friction, forces.lateralForce);

            _rb.AddForce(forces.friction, ForceMode.Force);
            _rb.AddForce(forces.lateralForce, ForceMode.Force);
        }

        private RaycastHit? GetGroundHitInfo()
        {
            RaycastHit hitInfo;
            bool hitGround = Physics.Raycast(
                origin: transform.position,
                direction: Vector3.down,
                hitInfo: out hitInfo,
                maxDistance: _rayCastDistance,
                layerMask: laneLayerMask,
                queryTriggerInteraction: QueryTriggerInteraction.Collide
            );

            return hitGround ? hitInfo : null;
        }

        private void DebugLogInfo(RaycastHit hitInfo, Vector3 friction, Vector3 lateralVelocity)
        {
            var surfaceFriction = hitInfo.collider.material.dynamicFriction;

            Debug.Log($"Surface: {hitInfo.collider.gameObject.name}, Friction: {surfaceFriction}");
            Debug.Log($"Friction Force: {friction}");
            Debug.Log($"Lateral Friction: {_lateralFriction * surfaceFriction}");
        }

        private (Vector3 friction, Vector3 lateralForce) CalculateBallMotionForces(RaycastHit hitInfo)
        {
            float surfaceFriction = hitInfo.collider.material.dynamicFriction;

            Vector3 frictionForce = -_rb.linearVelocity.normalized * 
                                     Physics.gravity.magnitude *
                                     surfaceFriction; 
            
            Vector3 lateralForce = _rb.angularVelocity.y * 
                                      _lateralFriction *
                                      surfaceFriction *
                                      transform.right; 

            return (frictionForce, lateralForce);
        }

        private (Vector3 throwForce, Vector3 spinTorque) CalculateReleaseForces(float power)
        {
            Vector3 throwForce = _forceMultiplier * power * Vector3.forward;
            Vector3 spinTorque = _spinMultiplier * Vector3.up;

            return (throwForce, spinTorque);
        }

        private void ApplyThrowForcesToBall(Vector3 throwForce, Vector3 spinTorque)
        {
            _rb.AddForce(throwForce, ForceMode.Impulse);
            _rb.AddTorque(spinTorque, ForceMode.Impulse);
        }
    }
}