namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using UnityEngine.Events;
    using NaughtyAttributes;

    /// <summary>
    /// Base class for weapons
    /// </summary>
    public abstract class Weapon : MonoBehaviour {
        #region Settings
        [Header("Weapon Settings")]
        [SerializeField] protected float reloadTime = 0.2f;
        [SerializeField] private bool continuousAttack = false;
        [SerializeField] private bool startWithRandomReloadTime = false;
        #endregion

        #region Events
        [SerializeField, Foldout("Events")] private UnityEvent onExecute = new UnityEvent();
        #endregion

        #region Currents
        private TDSControler owner = null;
        protected float currentReloadTime = 0f;
        private bool holding = false;
        #endregion

        #region Properties
        public bool Reloaded => currentReloadTime > reloadTime;
        public bool Holding => holding;
        public bool ContinuousAttack => continuousAttack;
        public TDSControler Owner => owner;
        #endregion

        #region Initialization
        public void BindToControler(TDSControler owner) {
            this.owner = owner;
            transform.parent = owner.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        #endregion

        #region Callbacks
        private void OnEnable() {
            holding = false;
            currentReloadTime = reloadTime;
        }

        private void Update() {
            if(!Reloaded) {
                currentReloadTime += Time.deltaTime;
            }

            if (holding && Reloaded) {
                Execute();
                onExecute?.Invoke();
                if (!continuousAttack) {
                    holding = false;
                }
                currentReloadTime = 0f;
            }
        }
        #endregion

        #region Actions
        public void Trigger() {
            if (startWithRandomReloadTime) {
                currentReloadTime = Random.Range(0f, reloadTime);
            }
            holding = true;
        }

        protected abstract void Execute();

        public virtual void Release() {
            holding = false;
        }
        #endregion
    }
}