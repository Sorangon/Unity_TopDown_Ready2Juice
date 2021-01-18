namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Loot objects at position on death
    /// </summary>
    [CreateAssetMenu(fileName = "newLootObjects", menuName = "Top Down Shooter/Death Behaviours/Loot Objects", order = 0)]
    public class DieAndLootObjects : DeathBehaviour {
        #region Settings
        [SerializeField] private float lootRadius = 1f;
        [SerializeField] private LootableObject[] lootableObjects = { };
        #endregion

        #region Sub classes
        [System.Serializable]
        private class LootableObject {
            public GameObject lootableObject = null;
            [Min(0)] public Vector2Int countRange = new Vector2Int(1, 3);
            [Range(0f, 1f)] public float spawnProbability = 1f;
        }
        #endregion

        #region Behaviour
        public override void Execute(Health ownerHealthSystem, Object target) {
            foreach (var lo in lootableObjects) {
                if (Random.value <= lo.spawnProbability) {
                    int instancesCount = Random.Range(lo.countRange.x, lo.countRange.y);
                    for (int i = 0; i < instancesCount; i++) {
                        Vector3 position = new Vector3(Random.value, 0f, Random.value).normalized;
                        position *= Random.value * lootRadius;
                        Instantiate(lo.lootableObject, position + ownerHealthSystem.transform.position, Quaternion.identity);
                    }
                }
            }

            Destroy(ownerHealthSystem.gameObject);
        }
        #endregion
    }
}
