using UnityEngine;

namespace pin
{
    public class Pin : MonoBehaviour
    {
        [SerializeField] private PinConfig _config;
        public PinConfig PinConfig { get => _config; set => _config = value; }

        private Vector3 _initialPosition;
        private Rigidbody _rb;

        public bool IsFallen => transform.up.y < PinConfig.FallThreshold;
        public bool IsSettled
        {
            get => _rb.linearVelocity.magnitude < PinConfig.MinimumLinearVelocity &&
                   _rb.angularVelocity.magnitude < PinConfig.MinimumAngularVelocity;
        }
        
        void Start()
        {
            _initialPosition = transform.position;
            _rb = GetComponent<Rigidbody>();
            if (_rb == null)
            {
                Debug.LogError("Rigidbody needs to be added to the Pin", this);
            }
        }

        public void ResetPin()
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.SetPositionAndRotation(_initialPosition, Quaternion.identity);
            gameObject.SetActive(true);
        }
    }
}