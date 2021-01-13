namespace TopDownShooter.Gameplay {
    using UnityEngine;

    public class Projectile : MonoBehaviour {
        #region Physic Callbacks
        private void OnCollisionEnter(Collision collision) {
            Destroy(this.gameObject);
        }
        #endregion
    }
}