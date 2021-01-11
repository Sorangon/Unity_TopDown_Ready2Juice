namespace TopDownShooter.Gameplay {
    using System;
    using UnityEngine;

    /// <summary>
    /// Base TopDown camera
    /// </summary>
    [ExecuteAlways]
    public class TopDownCamera : MonoBehaviour {
        #region Settings
        [SerializeField] private Transform targetCharacter = null;
        [SerializeField] private Vector3 offset = Vector3.up * 5f;
        #endregion

        #region Callbacks
        private void LateUpdate() {
            SetOffsetTransform();
        }

        #endregion

        #region Transform
        private void SetOffsetTransform() {
            Vector3 offsetedPos = targetCharacter.position + offset;
            transform.position = offsetedPos;

            Quaternion lookAtRotation = Quaternion.LookRotation(targetCharacter.position - offsetedPos);
            transform.rotation = lookAtRotation;
        }
        #endregion
    }
}
