namespace TopDownShooter.Gameplay {
    using TopDownShooter.Utility;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Base spawner for player
    /// </summary>
    public class PlayerStart : SingletonMonoBehaviour<PlayerStart> {
        [SerializeField] private UnityEvent onSpawn;

        public void Spawn(TDSPlayerControler player) {
            player.transform.position = transform.position;
            onSpawn?.Invoke();
        }
    }
}