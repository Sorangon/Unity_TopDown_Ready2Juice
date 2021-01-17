namespace TopDownShooter.Utility {
    using UnityEngine;

    /// <summary>
    /// Base singleton class
    /// </summary>
    [DefaultExecutionOrder(-500)]
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
        #region Currents
        private static T instance = null;
        #endregion

        #region Propeties
        public static T Instance => instance;
        #endregion

        #region Callbacks
        protected virtual void Awake() {
            if(instance == null) {
                instance = this as T;
            } else {
                if(instance != this) {
                    Destroy(gameObject);
                }
            }
        }
        #endregion
    }
}
