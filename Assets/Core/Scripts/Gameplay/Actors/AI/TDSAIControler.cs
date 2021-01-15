namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using NaughtyAttributes;

    /// <summary>
    /// Simple AI Behaviour
    /// Targets the player, chase him and stops if too close
    /// </summary>
    public class TDSAIControler : MonoBehaviour {
        #region Contants
        private const float UPDATE_RATE = 0.15f;
        #endregion

        #region Settings
        [SerializeField] private Weapon weapon = null;

        [Header("Movement")]
        [SerializeField] private float stopDistance = 3f;
        [SerializeField, Required] private TDSAIMovements movements = null;

        [Header("Sight")]
        [SerializeField] private float sightRadius = 1f;
        [SerializeField] private LayerMask sightLayers = new LayerMask();
        #endregion

        #region Current
        private float currentUpdateTimer = 0f;
        #endregion

        #region Callbacks
        private void Start() {
            if (TDSPlayerControler.CurrentControlerInstance != null) {
                movements.SetTarget(TDSPlayerControler.CurrentControlerInstance.transform);
            }

            currentUpdateTimer = Random.Range(0f, UPDATE_RATE);
        }

        private void Update() {
            TDSPlayerControler target = TDSPlayerControler.CurrentControlerInstance;
            currentUpdateTimer += Time.deltaTime;

            if (!ReferenceEquals(target, null)) {

                //Check target is expensive, we don't necessarily need to update it every frame
                if(currentUpdateTimer > UPDATE_RATE) {
                    bool seeTarget = SeeTarget(target.transform.position);
                    UpdateMovementState(target.transform.position, seeTarget);
                    currentUpdateTimer = 0f;
                }
            }
        }
        #endregion

        #region Behaviour
        private void UpdateMovementState(Vector3 targetPos, bool seeTarget) {
            //Should not stop if can't see the target
            float targetSqrStopDistance = seeTarget ? (stopDistance * stopDistance) : 0f;

            //Square distance is less expensive to calculate 
            //(square root method in vector length formula is expensive and isn't calculated in square distance)
            bool isClose = Vector3.SqrMagnitude(targetPos - transform.position) < targetSqrStopDistance;

            if (isClose && !movements.IsStopped) {
                movements.SetActiveMovement(false);
            } else if (!isClose && movements.IsStopped) {
                movements.SetActiveMovement(true);
            }
        }
        #endregion

        #region View
        private bool SeeTarget(Vector3 target) {
            Vector3 viewDir = target - transform.position;
            Ray ray = new Ray(transform.position, viewDir);
            if(Physics.SphereCast(ray, sightRadius, out RaycastHit hit, viewDir.magnitude, sightLayers)) {
#if UNITY_EDITOR
                Debug.DrawLine(transform.position, hit.point, Color.red, UPDATE_RATE);
#endif
                return false;
            } 
#if UNITY_EDITOR
            else {
                Debug.DrawLine(transform.position, target, Color.green, UPDATE_RATE); 
            }
#endif
            return true;
        }
        #endregion
    }
}