namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using NaughtyAttributes;

    /// <summary>
    /// Simple AI Behaviour
    /// Targets the player, chase him and stops if too close
    /// </summary>
    public class TDSAIControler : MonoBehaviour {
        #region Settings
        [SerializeField] private float stopDistance = 3f;
        [SerializeField] private Weapon weapon = null;
        [SerializeField, Required] private TDSAIMovements movements = null;
        #endregion

        #region Callbacks
        private void Start() {
            if (TDSPlayerControler.CurrentControlerInstance != null) {
                movements.SetTarget(TDSPlayerControler.CurrentControlerInstance.transform);
            }
        }

        private void Update() {
            if (!ReferenceEquals(TDSPlayerControler.CurrentControlerInstance, null)) {
                UpdateMovementState();
            }
        }
        #endregion

        #region Behaviour
        private void UpdateMovementState() {
            Vector3 playerPos = TDSPlayerControler.CurrentControlerInstance.transform.position;

            //Square distance is less expensive to calculate 
            //(square root method in vector length formula is expensive and isn't calculated in square distance)
            bool isClose = Vector3.SqrMagnitude(playerPos - transform.position) < (stopDistance * stopDistance);

            if (isClose && !movements.IsStopped) {
                Debug.Log("Close");
                movements.SetActiveMovement(false);
            } else if (!isClose && movements.IsStopped) {
                movements.SetActiveMovement(true);
            }
        }
        #endregion
    }
}