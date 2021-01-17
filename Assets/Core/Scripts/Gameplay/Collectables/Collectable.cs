namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Base class for items that can collected
    /// </summary>
    public abstract class Collectable : MonoBehaviour {
        #region Settings
        [SerializeField] private string targetLayer = "Default";
        #endregion

        #region Physics Callbacks
        protected virtual void OnTriggerEnter(Collider col) {
            if (col.gameObject.layer == LayerMask.NameToLayer(targetLayer)) {
                GameObject collector;
                if(!ReferenceEquals(col.attachedRigidbody, null)) {
                    collector = col.attachedRigidbody.gameObject;
                } else {
                    collector = col.gameObject;
                }

                OnCollect(collector);

                Destroy(gameObject);
            }
        }
        #endregion

        #region Collect
        protected abstract void OnCollect(GameObject collector);
        #endregion
    }
}