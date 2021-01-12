namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Base character for Top Down Shooter
    /// </summary>
    public class TDSCharacterMovements : MonoBehaviour {
        #region Settings
        [Header("Movement")]
        [Min(0f), SerializeField] private float movementSpeed = 8f;
        [Min(0f), SerializeField] private float accelerationTime = 0.3f;
        [Min(0f), SerializeField] private float decelerationTime = 0.1f;

        [Header("References")]
        [SerializeField] private new Rigidbody rigidbody = null;
        #endregion

        #region Currents
        /// <summary> Movement direction on XZ plane </summary>
        public Vector2 MovementDirection { get; set; } = Vector2.zero;

        private Vector3 velocity = Vector3.zero;
        private Vector3 movementVector = Vector3.zero;
        private float currentSpeedRatio = 0f;
        #endregion

        #region Callbacks
        private void FixedUpdate() {
            CalculateMovement();
            rigidbody.velocity = velocity * currentSpeedRatio;
        }
        #endregion

        #region Movement
        private void CalculateMovement() {
            float movementMagnitude = MovementDirection.magnitude;

            if(MovementDirection.sqrMagnitude > 0f) {
                movementVector = new Vector3(MovementDirection.x, 0f, MovementDirection.y);
            }

            if (movementMagnitude >= currentSpeedRatio && movementMagnitude > 0f) {
                currentSpeedRatio = Mathf.Min(currentSpeedRatio + Time.deltaTime/accelerationTime , 1f);
            } else {
                currentSpeedRatio = Mathf.Max(currentSpeedRatio - Time.deltaTime / decelerationTime, 0f);
            }

            velocity = movementVector.normalized * movementSpeed * currentSpeedRatio;
        }
        #endregion
    }
}