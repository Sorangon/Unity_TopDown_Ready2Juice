﻿namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using NaughtyAttributes;

    /// <summary>
    /// Base class for projectile
    /// </summary>
    public class Projectile : MonoBehaviour {
        #region Settings
        [SerializeField, Min(0)] private int damages = 20;
        [SerializeField, Required] private new Collider collider = null;
        #endregion

        #region Setup
        public void SetIgnoredCollider(Collider ignoredCol) {
            Physics.IgnoreCollision(collider, ignoredCol);
        }
        #endregion

        #region Physic Callbacks
        private void OnCollisionEnter(Collision collision) {
            Component root = collision.rigidbody;
            if(ReferenceEquals(root, null)) {
                root = collision.collider;
            }

            if (root.gameObject.TryGetComponent(out Health health)) {
                health.InflictDamages(damages);
            }
            Destroy(this.gameObject);
        }
        #endregion
    }
}