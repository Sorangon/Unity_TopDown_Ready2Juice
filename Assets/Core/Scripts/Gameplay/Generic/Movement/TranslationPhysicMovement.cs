namespace TopDownShooter.Gameplay {
    using UnityEngine;
    public class TranslationPhysicMovement : MonoBehaviour {
        #region Settings
        [Header("Settings")]
        public Vector3 direction = Vector3.forward;
        public float speedMultiplier = 1f;
        [SerializeField] private bool worldSpaceRelative = false;

        [Header("References")]
        [SerializeField] private new Rigidbody rigidbody = null;
        #endregion

        #region Callbacks
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