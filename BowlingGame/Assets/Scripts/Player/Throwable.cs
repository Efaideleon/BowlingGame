using UnityEngine;

namespace player {
    public abstract class Throwable : MonoBehaviour {
        public virtual void Hold(Transform parent) { }
        public virtual void Swing(Transform parent) { }
        public virtual void Throw() { }
        public virtual void Throw(float power, Vector3 throwDirection) { }
    }
}