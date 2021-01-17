namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Loot objects at position on death
    /// </summary>
    [CreateAssetMenu(fileName = "newLootObjects", menuName = "Top Down Shooter/Death Behaviours/Loot Objects", order = 1000)]
    public class LootObjects : DeathBehaviour {
        #region Settings

        #endregion

        #region Sub classes
        private class LootableObject {
            //public 
        }
        #endregion

        #region Behaviour
        public override void Execute(Object target) {
            Debug.Log("Loot");
        } 
        #endregion
    }
}
