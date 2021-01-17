namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Heals the player on trigger
    /// </summary>
    public class HealCollectable : Collectable {
        #region Settings
        [SerializeField, Min(0f)] private int healAmount = 50;

        protected override void OnCollect(GameObject collector) {
            collector.GetComponent<Health>().Heal(healAmount);
        }
        #endregion
    }
}