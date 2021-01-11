namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Base player controler for Top Down Shooter
    /// </summary>
    public class TDSPlayerControler : MonoBehaviour {
        #region Settings
        [SerializeField] private TDSCharacterMovements targetCharacter = null;
        [SerializeField] private Weapon currentWeapon = null;
        #endregion

        #region Callbacks
        private void Start() {
            
        }
        #endregion
    }
}