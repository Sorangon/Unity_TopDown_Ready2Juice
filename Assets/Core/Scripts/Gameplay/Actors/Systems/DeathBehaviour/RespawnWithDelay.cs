namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Respawns the character with a delay
    /// </summary>
    [CreateAssetMenu(fileName = "newRespawnWithDelay", menuName = "Top Down Shooter/Death Behaviours/Respawn With Delay", order = 1000)]
    public class RespawnWithDelay : DeathBehaviour {
        #region Datas
        [SerializeField] private float respawnDelay = 1f;
        [SerializeField] private float invincibilityTime = 5f;
        #endregion

        #region Currents
        TDSPlayerControler targetPlayer = null;
        #endregion

        #region Behaviour
        public override void Execute(Object target) {
            if (target is TDSPlayerControler) {
                targetPlayer = target as TDSPlayerControler;
                targetPlayer.gameObject.SetActive(false);
                Timer t = new Timer(respawnDelay, Respawn);
                t.Play();
            } else {
                Debug.LogError("Respawn With Delay Death Behaviour must take a TDS Player Controler parameter");
            }
        }

        private void Respawn() {
            targetPlayer.gameObject.SetActive(true);
            targetPlayer.transform.position = PlayerStart.Instance.transform.position;
            targetPlayer.HealthSystem.Heal(targetPlayer.HealthSystem.MaxHealth);

            //Invicibility time
            targetPlayer.HealthSystem.invicible = true;
            Timer invicibilityTimer = new Timer(respawnDelay, () => { targetPlayer.HealthSystem.invicible = false; });
            invicibilityTimer.Play();
        }
        #endregion
    }
}