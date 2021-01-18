namespace TopDownShooter.Gameplay {
    using UnityEngine;

    public class WeaponSelector : MonoBehaviour {
        #region References
        [SerializeField] private Weapon[] weapons = { };
        [SerializeField] private TDSPlayerControler targetPlayerControler = null;
        #endregion

        #region Currents
        private int currentWeaponId = 0;
        #endregion

        #region Callbacks
        private void OnEnable() {
            weapons[0].gameObject.SetActive(true);
            targetPlayerControler.SetWeapon(weapons[0]);

            for (int i = 1; i < weapons.Length; i++) {
                weapons[i].gameObject.SetActive(false);
            }
        }
        #endregion

        #region Switch
        public void SwitchWeapon() {
            weapons[currentWeaponId].gameObject.SetActive(false);

            currentWeaponId++;
            if(currentWeaponId >= weapons.Length) {
                currentWeaponId = 0;
            }
            
            weapons[currentWeaponId].gameObject.SetActive(true);
            targetPlayerControler.SetWeapon(weapons[currentWeaponId]);
        }
        #endregion
    }
}