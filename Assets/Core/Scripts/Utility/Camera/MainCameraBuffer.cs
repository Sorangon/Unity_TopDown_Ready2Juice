namespace TopDownShooter.Utility {
    using UnityEngine;
    using NaughtyAttributes;

    /// <summary>
    /// Stores the main camera in a static reference value
    /// </summary>
    public class MainCameraBuffer : MonoBehaviour {
        [SerializeField, Required] private new Camera camera = null;

        private static Camera mainCamera = null;

        public static Camera Get() {
            return mainCamera;
        }

        private void OnEnable() {
            mainCamera = camera;
        }
    }

}