namespace TopDownShooter.Utility {
    using UnityEngine;

    public class SetRotation : MonoBehaviour {
        public Vector3 eulerRotation = Vector3.zero;
        public bool worldSpace = false;

        public void Execute() {
            Quaternion rotation = Quaternion.Euler(eulerRotation);
            if (worldSpace) {
                transform.rotation = rotation;
            } else {
                transform.localRotation = rotation;
            }
        }
    }
}