namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using TopDownShooter.Tools;
    using UnityEngine.Events;
    using NaughtyAttributes;

    /// <summary>
    /// Base class for items that can collected
    /// </summary>
    public abstract class Collectable : MonoBehaviour {
        #region Settings
        [Header("Collectable Settings")]
        [SerializeField, LayerField] private int targetLayer = 0;

        [SerializeField, Foldout("Events")] UnityEvent onCollect = null; 
        #endregion

        #region Physics Callbacks
        protected virtual void OnTriggerEnter(Collider col) {
            if (col.gameObject.layer == targetLayer) {
                GameObject collector;
                if(!ReferenceEquals(col.attachedRigidbody, null)) {
                    collector = col.attachedRigidbody.gameObject;
                } else {
                    collector = col.gameObject;
                }

                OnCollect(collector);
                onCollect?.Invoke();
                Destroy(gameObject);
            }
        }
        #endregion

        #region Collect
        protected abstract void OnCollect(GameObject collector);
        #endregion
    }
}