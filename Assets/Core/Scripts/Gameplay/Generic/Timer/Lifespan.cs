namespace TopDownShooter.Gameplay {
    using UnityEngine;

    public class Lifespan : MonoBehaviour {
        #region Settings
        [SerializeField] float lifespan = 2f;
        [SerializeField] bool sendToPool = false;
        #endregion

        #region Callbacks
        void Update() {
            lifespan -= Time.deltaTime;
            if(lifespan < 0f) {
                if (sendToPool) {
                    //TODO : Create a pooling system
                    Destroy(gameObject);
                } else {
                    Destroy(gameObject);
                }
            }
        } 
        #endregion
    }
}