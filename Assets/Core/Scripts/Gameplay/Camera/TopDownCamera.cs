namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Base Top Down camera
    /// </summary>
    [ExecuteAlways]
    public class TopDownCamera : MonoBehaviour {
        #region Settings
        [Header("Target")]
        [SerializeField] private Transform target = null;
        [SerializeField] private Vector3 offset = Vector3.up * 5f;
        [SerializeField] private float damping = 0.5f;

        [Header("Aim Offset")]
        [SerializeField] private TDSPlayerControler playerControler = null;
        [SerializeField] private float aimOffsetLength = 2f;
        [SerializeField] private float aimOffsetDamping = 0.1f;
        #endregion

        #region Current
        private Vector3 currentCenterOfView = Vector3.zero;
        private Vector3 currentAimOffset = Vector3.zero;
        #endregion

        #region Callbacks
        private void Awake() {
            currentCenterOfView = target.position;
        }

        private void LateUpdate() {
            SetOffsetTransform();

            if(!ReferenceEquals(playerControler, null)) {
                AimDirectionOffset();
            }
        }
        #endregion

        #region Transform
        private void SetOffsetTransform() {
            if (damping > 0f) {
                currentCenterOfView = Vector3.Lerp(currentCenterOfView, target.position, Time.deltaTime / damping);
            } else {
                currentCenterOfView = target.position;
            }

            Vector3 offsetedPos = currentCenterOfView + offset;
            transform.position = offsetedPos;

            Quaternion lookAtRotation = Quaternion.LookRotation(currentCenterOfView - offsetedPos);
            transform.rotation = lookAtRotation;
        }

        private void AimDirectionOffset() {
            Vector3 aimOffset = playerControler.AimVector * aimOffsetLength;
            currentAimOffset = Vector3.Lerp(currentAimOffset, aimOffset, Time.deltaTime / aimOffsetDamping);
            transform.position += currentAimOffset;
        }
        #endregion
    }
}
