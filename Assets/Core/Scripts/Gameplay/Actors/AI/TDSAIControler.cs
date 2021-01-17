namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using NaughtyAttributes;

    /// <summary>
    /// Simple AI Behaviour
    /// Targets the player, chase him and stops if too close
    /// </summary>
    public class TDSAIControler : TDSControler {
        #region Contants
        private const float UpdateRate = 0.15f;
        #endregion

        #region Settings
        [SerializeField] private Weapon weapon = null;

        [Header("Movement")]
        [SerializeField] private float stopDistance = 3f;
        [SerializeField, Min(0f)] private float focusPlayerDamping = 0.2f;
        [SerializeField, Required] private TDSAIMovements movements = null;

        [Header("Attack")]
        [SerializeField] private float attackRange = 6f;

        [Header("Sight")]
        [SerializeField] private float sightRadius = 1f;
        [SerializeField] private LayerMask sightLayers = new LayerMask();
        #endregion

        #region Current
        private float currentUpdateTimer = 0f;
        private bool attacking = false;
        #endregion

        #region Callbacks
        private void Start() {
            if (TDSPlayerControler.CurrentControlerInstance != null) {
                movements.SetTarget(TDSPlayerControler.CurrentControlerInstance.transform);
            }

            currentUpdateTimer = Random.Range(0f, UpdateRate);
            if (weapon) {
                weapon.BindToControler(this);
            }
        }

        private void Update() {
            TDSPlayerControler target = TDSPlayerControler.CurrentControlerInstance;
            currentUpdateTimer += Time.deltaTime;

            if (!ReferenceEquals(target, null)) {
                //Check target is expensive, we don't necessarily need to update it every frame
                if(currentUpdateTimer > UpdateRate) {
                    bool isSeeingTarget = SeeTarget(target.transform.position);
                    float sqrAttackRange = attackRange * attackRange;

                    bool canAttack = Vector3.SqrMagnitude(transform.position - target.transform.position) < sqrAttackRange && isSeeingTarget;

                    if (canAttack != attacking) {
                        attacking = canAttack;

                        //Nav Mesh Agent must stop updating its rotation if target is seen
                        movements.AttachedNavMeshAgent.updateRotation = !canAttack;

                        if(!ReferenceEquals(weapon, null)) {
                            if (attacking) {
                                weapon.Trigger();
                            } else {
                                weapon.Release();
                            }
                        }
                    }

                    UpdateMovementState(target.transform.position, isSeeingTarget);
                    currentUpdateTimer = 0f;
                }

                if (attacking) {
                    LookAtTarget();
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

        private void LookAtTarget() {
            Vector3 targetPos = TDSPlayerControler.CurrentControlerInstance.transform.position;
            aimVector = targetPos - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(aimVector, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime / focusPlayerDamping);
        }
        #endregion

        #region View
        private bool SeeTarget(Vector3 target) {
            Vector3 viewDir = target - transform.position;
            Ray ray = new Ray(transform.position, viewDir);
            if(Physics.SphereCast(ray, sightRadius, out RaycastHit hit, viewDir.magnitude, sightLayers)) {
#if UNITY_EDITOR
                Debug.DrawLine(transform.position, hit.point, Color.red, UpdateRate);
#endif
                return false;
            } 
#if UNITY_EDITOR
            else {
                Debug.DrawLine(transform.position, target, Color.green, UpdateRate); 
            }
#endif
            return true;
        }
        #endregion
    }
}