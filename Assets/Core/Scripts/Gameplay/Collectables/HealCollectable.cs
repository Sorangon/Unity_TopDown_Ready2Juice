namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Heals the player on trigger
    /// </summary>
    public class HealCollectable : Collectable {
        #region Settings
        [Header("Health")]
        [SerializeField, Min(0f)] private int healAmount = 50;
        #endregion

        #region Collect
        protected override void OnCollect(GameObject collector) {
            collector.GetComponent<Health>().Heal(healAmount);
        }
        #endregion
    }
}