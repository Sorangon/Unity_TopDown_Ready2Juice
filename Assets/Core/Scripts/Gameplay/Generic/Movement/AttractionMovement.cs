namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Gets attracted by the first triggered object 
    /// </summary>
    public class AttractionMovement : MonoBehaviour {
        #region Settings
        [Header("Settings")]
        [SerializeField] private float attractionSpeed = 3f;

        [Header("References")]
        [SerializeField] private Transform root = null;
        #endregion

        #region Currents
        private Transform target = null;
        private bool attracted = false;
        #endregion

        #region Callbacks
        private void FixedUpdate() {
            if (attracted) {
                root.position += (target.position - root.position).normalized * attractionSpeed * Time.fixedDeltaTime;
            }            
        }
        #endregion

        #region Collision Callbacks
        private void OnTriggerEnter(Collider other) {
            if (!attracted) {
                target = other.transform;
                attracted = true;
            }
        }
        #endregion
    }
}