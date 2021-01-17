﻿namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// A projectile that inflict damages in a range a impact
    /// </summary>
    public class ExplosiveProjectile : Projectile {
        #region Settings
        [SerializeField] private float explosionRadius = 2f;
        #endregion

        #region Currents
        static Collider[] collisions = new Collider[40];
        #endregion

        #region Impact
        protected override void OnImpact(Collision collision) {
            int collisionsCount = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, collisions, LayerUtility.GetLayerMask(gameObject.layer));
            for (int i = 0; i < collisionsCount; i++) {
                GameObject root = GetRootGameObject(collisions[i]);

                if(TryGetHealthComponent(root, out Health health)){
                    health.InflictDamages(damages);
                }
                if(root.TryGetComponent(out Projectile proj)) {
                    Destroy(proj.gameObject);
                }
            }
        }
        #endregion

#if UNITY_EDITOR
        #region Editor
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
        #endregion  
#endif
    }
}