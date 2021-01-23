namespace TopDownShooter.Utility {
    using UnityEngine;

    public class Rotation : MonoBehaviour {
        [SerializeField] private Vector3 eulerRotation = Vector3.up;

        Quaternion currentRot = Quaternion.identity;

        void Update() {
            currentRot *= Quaternion.Euler(eulerRotation * Time.deltaTime);
            transform.rotation = currentRot;
        }
    }

}