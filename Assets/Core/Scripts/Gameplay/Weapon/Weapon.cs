namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Base class for weapons
    /// </summary>
    public abstract class Weapon : MonoBehaviour {
        #region Settings
        [Header("Weapon Settings")]
        [SerializeField] protected float reloadTime = 0.2f;
        [SerializeField] private bool continuousAttack = false;
        #endregion

        #region Currents
        private TDSPlayerControler owner = null;
        protected float currentReloadTime = 0f;
        private bool holding = false;
        #endregion

        #region Properties
        public bool Reloaded => currentReloadTime > reloadTime;
        public TDSPlayerControler Owner => owner;
        #endregion

        #region Initialization
        public void BindToControler(TDSPlayerControler owner) {
            this.owner = owner;
            transform.parent = owner.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        #endregion

        #region Callbacks
        private void Update() {
            if(!Reloaded) {
                currentReloadTime += Time.deltaTime;
            }

            if (holding && Reloaded) {
                Execute();
                if (!continuousAttack) {
                    holding = false;
                }
                currentReloadTime = 0f;
            }
        }
        #endregion

        #region Actions
        public void Trigger() {
            holding = true;
        }

        protected abstract void Execute();

        public virtual void Release() {
            holding = false;
        }
        #endregion
    }
}