namespace TopDownShooter.Utility {
    using UnityEngine;

    public class CameraYRelative : MonoBehaviour {
        private void LateUpdate() {
            Vector3 camForward = MainCameraBuffer.Get().transform.forward;
            camForward.y = 0f;

            float angle = Mathf.Atan2(camForward.x, camForward.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }
}