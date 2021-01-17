namespace TopDownShooter.Gameplay {
    using UnityEngine;

    public class Lifespan : MonoBehaviour {
        #region Settings
        [SerializeField] float lifespan = 2f;
        #endregion

        #region Callbacks
        void Update() {
            lifespan -= Time.deltaTime;
            if (lifespan < 0f) {
                Destroy(gameObject);
            }
        }
        #endregion
    }
}