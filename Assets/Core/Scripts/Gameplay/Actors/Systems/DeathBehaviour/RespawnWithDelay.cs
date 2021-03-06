﻿namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using TopDownShooter.Utility;

    /// <summary>
    /// Respawns the character with a delay
    /// </summary>
    [CreateAssetMenu(fileName = "newRespawnWithDelay", menuName = "Top Down Shooter/Death Behaviours/Respawn With Delay", order = 0)]
    public class RespawnWithDelay : DeathBehaviour {
        #region Datas
        [SerializeField] private float respawnDelay = 1f;
        [SerializeField] private float invincibilityTime = 5f;
        #endregion

        #region Currents
        TDSPlayerControler targetPlayer = null;
        #endregion

        #region Behaviour
        public override void Execute(Health ownerHealthSystem, Object target) {
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
            targetPlayer.HealthSystem.Heal(targetPlayer.HealthSystem.MaxHealth);
            PlayerStart.Instance.Spawn(targetPlayer);
            targetPlayer.gameObject.SetActive(true);

            //Invicibility time
            targetPlayer.HealthSystem.invicible = true;
            Timer invicibilityTimer = new Timer(invincibilityTime, () => { targetPlayer.HealthSystem.invicible = false; });
            invicibilityTimer.Play();
        }
        #endregion
    }
}