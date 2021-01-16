namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Base class for all controlers
    /// </summary>
    public class TDSControler : MonoBehaviour {
        #region Current
        protected Vector3 aimVector = Vector3.zero;
        #endregion

        #region Properties
        public Vector3 AimVector => aimVector;
        public float AimAngle => Mathf.Atan2(AimVector.x, AimVector.z) * Mathf.Rad2Deg;
        #endregion
    }
}