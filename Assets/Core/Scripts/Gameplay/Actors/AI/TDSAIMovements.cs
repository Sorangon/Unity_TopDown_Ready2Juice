namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using UnityEngine.AI;
    using NaughtyAttributes;

    /// <summary>
    /// Manage AI movement with nav mesh
    /// </summary>
    public class TDSAIMovements : MonoBehaviour {
        #region Constants
        private const float REFRESH_TARGET_RATE = 0.4f;
        #endregion

        #region Settings
        [SerializeField, Required] private NavMeshAgent navMeshAgent = null;
        [SerializeField] private Transform currentTarget = null;
        #endregion

        #region Currents
        private float currentRefreshTimer = 0f;
        public bool IsStopped => navMeshAgent.isStopped;
        #endregion

        #region Callbacks
        private void Start() {
            if (currentTarget != null) {
                navMeshAgent.SetDestination(currentTarget.position);
            }

            currentRefreshTimer = Random.Range(0f, REFRESH_TARGET_RATE);
            navMeshAgent.updateRotation = false;
        }

        private void Update() {
            if (!navMeshAgent.isStopped) {
                //Performance tip
                //We don't need to update the nav mesh target each frame
                //Put a timer will reduce the number of times the navmesh agent will retarget a position
                currentRefreshTimer += Time.deltaTime;
                if (currentRefreshTimer > REFRESH_TARGET_RATE) {
                    if (currentTarget != null) {
                        navMeshAgent.SetDestination(currentTarget.position);
                    }
                    currentRefreshTimer = 0f;
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