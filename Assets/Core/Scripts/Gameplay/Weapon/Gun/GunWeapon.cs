namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using NaughtyAttributes;

    /// <summary>
    /// Can shoot projectiles in a target direction
    /// </summary>
    public class GunWeapon : Weapon{
        #region Settings
        [Header("Gun Settings")]
        [SerializeField] private float spread = 3f;
        [SerializeField, Required] private Projectile projectile;
        [SerializeField] private Transform muzzleTransform;
        #endregion

        #region Shoot
        protected override void Execute() {
            float spreadAngle = 0f;
            if(spread > 0f) {
                spreadAngle = Random.Range(-spread * 0.5f, spread * 0.5f);
            }
            Quaternion projRotation = Quaternion.AngleAxis(Owner.AimAngle + spreadAngle, Vector3.up);
            Projectile instance = Instantiate(projectile, muzzleTransform.position, projRotation);
        }
        #endregion
    }
}