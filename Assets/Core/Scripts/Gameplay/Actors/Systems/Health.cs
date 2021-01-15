namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using NaughtyAttributes;

    /// <summary>
    /// Base health system for game objects
    /// </summary>
    public class Health : MonoBehaviour {
        #region Settings
        [SerializeField, Min(0)] private int maxHealth = 100;
        [Header("Death")]
        [SerializeField] private DeathBehaviour deathBehaviour = null;
        [SerializeField] private Object deathBehaviourParameter = null;
        #endregion

        #region Currents
        [ShowNonSerializedField]
        private int currentHealth = 100; 
        #endregion

        #region Properties
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        public float CurrentHealthRatio => (float)currentHealth / (float)maxHealth;
        #endregion

        #region Callbacks
        private void Start() {
            currentHealth = maxHealth;
        }
        #endregion

        #region Update Health
        public void Heal(int amount) {
            if(amount < 0) {
                return;
            }

            currentHealth += amount; 
            if(currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
        }

        public void InflictDamages(int amount) {
            if(amount < 0) {
                return;
            }

            currentHealth -= amount;
            if(currentHealth <= 0) {
                Die();
            }
        }

        public void AddMaxHealth(int amount, float healthPercentageToAdd = 0f) {
            maxHealth += amount;

            if(healthPercentageToAdd > 0) {
                currentHealth += Mathf.CeilToInt(amount * healthPercentageToAdd);
            }
        }
        #endregion

        #region Die
        [ContextMenu("Die")]
        public void Die() {
            if(deathBehaviour != null) {
                deathBehaviour.Execute(deathBehaviourParameter);
            } else {
                Destroy(gameObject);
            }
        }
        #endregion
    }
}