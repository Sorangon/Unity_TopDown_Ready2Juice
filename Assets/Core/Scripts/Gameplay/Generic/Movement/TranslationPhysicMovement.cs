namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using NaughtyAttributes;

    public class TranslationPhysicMovement : MonoBehaviour {
        #region Settings
        [Header("Settings")]
        public Vector3 direction = Vector3.forward;
        public float speedMultiplier = 1f;
        [SerializeField] private bool worldSpaceRelative = false;

        [Header("References")]
        [SerializeField, Required] private new Rigidbody rigidbody = null;
        #endregion

        #region Callbacks
        private void OnEnable() {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void FixedUpdate() {
            Vector3 dir = direction;
            if (!worldSpaceRelative) {
                dir = transform.rotation * direction;
            }

            rigidbody.velocity = dir * speedMultiplier;
        }
        #endregion
    }
}