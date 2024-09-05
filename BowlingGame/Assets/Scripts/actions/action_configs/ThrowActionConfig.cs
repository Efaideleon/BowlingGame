using UnityEngine;

namespace player {
    [CreateAssetMenu(fileName = "ThrowAction", menuName = "Actions/ThrowAction")]
    public class ThrowActionConfig : ScriptableObject {
        [Header("Settings")]
        [Tooltip("Hold long the throw actions lasts.")]
        [SerializeField] float _throwDuration;

        public float ThrowDuration => _throwDuration;
    }
}
