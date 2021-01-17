namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Base class for projectile
    /// </summary>
    public class Projectile : MonoBehaviour {
        #region Settings
        [SerializeField, Min(0)] protected int damages = 20;
        #endregion

        #region Physic Callbacks
        private void OnCollisionEnter(Collision collision) {
            OnImpact(collision);
            Destroy(this.gameObject);
        }
        #endregion

        #region Impact
        protected virtual void OnImpact(Collision collision) {
            if (TryGetHealthComponent(collision.collider, out Health health)) {
                health.InflictDamages(damages);
            }
        }
        #endregion

        #region Utility
        /// <summary>
        /// Returns the health component at the root of the GameObject
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected bool TryGetHealthComponent(Collider col, out Health health) {
            Component root = col.attachedRigidbody;
            if (ReferenceEquals(root, null)) {
                root = col;
            }

            if (root.gameObject.TryGetComponent(out Health healthComponent)) {
                health = healthComponent;
                return true;
            } else {
                health = null;
                return false;
            }
        }
        #endregion
    }
}