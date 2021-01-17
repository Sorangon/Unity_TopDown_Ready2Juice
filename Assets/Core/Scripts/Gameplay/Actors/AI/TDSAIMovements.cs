namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using UnityEngine.AI;
    using NaughtyAttributes;

    /// <summary>
    /// Manage AI movement with nav mesh
    /// </summary>
    public class TDSAIMovements : MonoBehaviour {
        #region Constants
        private const float UpdateRate = 0.4f;
        #endregion

        #region Settings
        [SerializeField, Required] private NavMeshAgent navMeshAgent = null;
        [SerializeField] private Transform currentTarget = null;
        #endregion

        #region Currents
        private float currentUpdateTimer = 0f;
        public bool IsStopped => navMeshAgent.isStopped;
        public NavMeshAgent AttachedNavMeshAgent => navMeshAgent;
        #endregion

        #region Callbacks
        private void Start() {
            if (currentTarget != null) {
                navMeshAgent.SetDestination(currentTarget.position);
            }

            currentUpdateTimer = Random.Range(0f, UpdateRate);
        }

        private void Update() {
            if (!navMeshAgent.isStopped) {
                //Performance tip
                //We don't need to update the nav mesh target each frame
                //Put a timer will reduce the number of times the navmesh agent will retarget a position
                currentUpdateTimer += Time.deltaTime;
                if (currentUpdateTimer > UpdateRate) {
                    if (currentTarget != null) {
                        navMeshAgent.SetDestination(currentTarget.position);
                    }
                    currentUpdateTimer = 0f;
                }
            }
        }
        #endregion

        #region Set Movement
        public void SetTarget(Transform target) {
            currentTarget = target;
        }

        public void SetActiveMovement(bool active) {
            navMeshAgent.isStopped = !active;
        }
        #endregion
    }
}