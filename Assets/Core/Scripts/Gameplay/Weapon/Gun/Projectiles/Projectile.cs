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
        private void OnTriggerEnter(Collider col) {
            OnImpact(col);
            DestroyProjectile();
        }
        #endregion

        #region Impact
        protected virtual void OnImpact(Collider collider) {
            if (TryGetHealthComponent(GetRootGameObject(collider), out Health health)) {
                health.InflictDamages(damages);
            }
        }

        public void DestroyProjectile() {
            Destroy(gameObject);
        }
        #endregion

        #region Utility
        /// <summary>
        /// Returns the health component at the root of the GameObject
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected bool TryGetHealthComponent(GameObject obj, out Health health) {
            if (obj.gameObject.TryGetComponent(out Health healthComponent)) {
                health = healthComponent;
                return true;
            } else {
                health = null;
                return false;
            }
        }

        /// <summary>
        /// The game object with the attached rigidbody or with the collider
        /// </summary>
        /// <returns></returns>
        protected GameObject GetRootGameObject(Collider col) {
            Component root = col.attachedRigidbody;
            if (ReferenceEquals(root, null)) {
                root = col;
            }

            return root.gameObject;
        }
        #endregion
    }
}