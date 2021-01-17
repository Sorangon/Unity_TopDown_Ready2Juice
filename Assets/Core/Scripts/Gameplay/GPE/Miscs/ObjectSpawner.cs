namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Spawn object at a fixed rate
    /// </summary>
    public class ObjectSpawner : MonoBehaviour {
        #region Settings
        [SerializeField] private GameObject prefab = null;
        [SerializeField, Min(0f)] private Vector2 randomRate = new Vector2(0.5f, 2f);
        [SerializeField] private bool parentObject = false;
        #endregion

        #region Currents
        private float currentTimer = 0f;
        #endregion

        #region Callbacks
        private void Start() {
            currentTimer = Random.Range(randomRate.x, randomRate.y);
        }

        private void Update() {
            currentTimer -= Time.deltaTime;

            if(currentTimer <= 0f) {
                currentTimer = Random.Range(randomRate.x, randomRate.y);
                Spawn();
            }
        }
        #endregion

        #region Spawn
        private void Spawn() {
            if (parentObject) {
                Instantiate(prefab, transform.position, transform.rotation, transform);
            } else {
                Instantiate(prefab, transform.position, transform.rotation);
            }
        }
        #endregion
    }
}