namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Base TopDown camera
    /// </summary>
    [ExecuteAlways]
    public class TopDownCamera : MonoBehaviour {
        #region Settings
        [SerializeField] private Transform target = null;
        [SerializeField] private Vector3 offset = Vector3.up * 5f;
        [SerializeField] private float damping = 0.5f;
        #endregion

        #region Current
        private Vector3 currentCenterOfView = Vector3.zero;
        #endregion

        #region Callbacks
        private void Awake() {
            currentCenterOfView = target.position;
        }

        private void LateUpdate() {
            SetOffsetTransform();
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
        #endregion
    }
}
